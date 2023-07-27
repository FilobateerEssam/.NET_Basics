
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitofwork;

        public CoverTypeController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            // to Retrieve All Data 
            IEnumerable<CoverType> objCoverTypeList = _unitofwork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        // GET Action
        public IActionResult Create()
        {
            return View();
        }


        // Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            
            // check if form have data or Not 
            if (ModelState.IsValid)
            {
                // to Add to DB

                _unitofwork.CoverType.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "CoverType created successfull";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        // GET Action
        public IActionResult Edit(int? id)
        {
            // Case ID Not Found

            if (id == null || id == 0)
            {
                return NotFound();
            }

            // If Found

            //var categortFromDb = _db.categories.Find(id);

            var CoverTypeFromDbFirst = _unitofwork.CoverType.GetFirstOrDefault(u => u.id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }


        // Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            
            // check if form have data or Not 
            if (ModelState.IsValid)
            {
                // to Update to DB

                _unitofwork.CoverType.Update(obj);
                _unitofwork.Save();
                TempData["success"] = "CoverType Updated successfull";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        // GET Action
        public IActionResult Delete(int? id)
        {
            // Case ID Not Found

            if (id == null || id == 0)
            {
                return NotFound();
            }

            // If Found

            //var categortFromDb = _db.categories.Find(id);

            var CoverTypeFromDbFirst = _unitofwork.CoverType.GetFirstOrDefault(u => u.id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }


        // Post Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitofwork.CoverType.GetFirstOrDefault(u => u.id == id);

            if (obj == null)
            {
                return NotFound();
            }


            // to Delete to DB

            _unitofwork.CoverType.Remove(obj);
            _unitofwork.Save();
            TempData["success"] = "CoverType Deleted successfull";

            return RedirectToAction("Index");

        }

    }
}
