using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MVC_DB_STOKEntities db = new MVC_DB_STOKEntities();
        //Burada paging işlemi yapılmıştır. Bunun yapılabilmesi için NuGet Package Manager üzerinden PagedList.Mvc indirildi ve yukarıda PagedList ve PagedList.Mvc eklendi.
        public ActionResult Index(int page=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList(); //eski kullanımımız.
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(page, 4); // başlangıç değerindeki sayfayı getir, her sayfada 4 satır veri getirilsin.
            return View(degerler);
        }

        [HttpGet] //sayfa yüklendiğinde
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost] //butona tıklandığında (httpget ve httppost kullanılarak, sayfa her yüklendiğinde null değer eklenmesi engellendi.
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            
            if (!ModelState.IsValid)//Modelin durumunda doğrulanma işlemi yapılmadıysa ("!" kullanılırsa olumsuzu oluyor yani yapılmadıysa)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var kategori= db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }     
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktgr = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktgr.KATEGORIAD = p1.KATEGORIAD;//ktgr den gelen kategori adına parametreden gelen p1 değerinin kategori adını ata.
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}