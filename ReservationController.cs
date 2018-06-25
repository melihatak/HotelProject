using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hotelproject.Models;

namespace hotelproject.Controllers
{
    public class ReservationController : Controller
    {
        private HotelDBEntities db = new HotelDBEntities();

        // GET: Reservation
        public ActionResult Index()
        {
            return View(db.hosted_at.Select(i => new ReservationVM() { hosted_at_id = i.id, room_type = i.occupied_room.room.room_type1.description, room = i.occupied_room.room.name, guest_name = (i.guest.firstname + " " + i.guest.lastname), checkin = i.occupied_room.reservation.date_in.ToString(), checkout = i.occupied_room.reservation.date_out.ToString(), made_by = i.occupied_room.reservation.user.name }));
        }

        public ActionResult Edit(Int32 hosted_at_id)
        {
            var hosted_at = db.hosted_at.FirstOrDefault(i => i.id == hosted_at_id);
            if(hosted_at != null)
            {
                return Json(new { firstname = hosted_at.guest.firstname, lastname = hosted_at.guest.lastname, gender = hosted_at.guest.gender, checkin = hosted_at.occupied_room.reservation.date_in.ToString(), checkout = hosted_at.occupied_room.reservation.date_out.ToString(), room = hosted_at.occupied_room.room.id }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Save(AddReservationVM item)
        {
            var guest = db.guest.Add(new guest() { firstname = item.firstname, lastname = item.lastname, gender = item.gender, member_since = DateTime.Now });
            db.SaveChanges();

            var reservation = db.reservation.Add(new reservation() { date_in = Convert.ToDateTime(item.checkin), date_out = Convert.ToDateTime(item.checkout), made_by = ((Session["user"] as Models.user).id), guest_id = guest.id });
            db.SaveChanges();

            var occupied_room = db.occupied_room.Add(new occupied_room() { check_in = new TimeSpan() { }, check_out = new TimeSpan() { }, room_id = item.room, reservation_id = reservation.id });
            db.SaveChanges();

            var hosted_at = db.hosted_at.Add(new hosted_at() { guest_id = guest.id, occupied_room_id = occupied_room.id });
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Update(AddReservationVM item, Int32 hosted_at_id)
        {
            var hosted_at = db.hosted_at.FirstOrDefault(i => i.id == hosted_at_id);
            if(hosted_at != null)
            {
                hosted_at.guest.firstname = item.firstname;
                hosted_at.guest.lastname = item.lastname;
                hosted_at.guest.gender = item.gender;
                hosted_at.occupied_room.reservation.date_in = Convert.ToDateTime(item.checkin);
                hosted_at.occupied_room.reservation.date_out = Convert.ToDateTime(item.checkout);
                hosted_at.occupied_room.room.id = item.room;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Int32 hosted_at)
        {
            var hosted = new hosted_at() {id = hosted_at };
            db.hosted_at.Attach(hosted);
            db.hosted_at.Remove(hosted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
