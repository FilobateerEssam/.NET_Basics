
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            // to Retrieve All Data 
            IEnumerable<Category> objCategoryList = _unitofwork.Category.GetAll();
            return View(objCategoryList);
        }

        // GET Action
        public IActionResult Create()
        {
            return View();
        }


        // Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplatOrder.ToString())
            {
                // Error place     // Msg will Display
                //ModelState.AddModelError("Customer Error", "The DisplayOrder cannot exactly match the Name");
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            // check if form have data or Not 
            if (ModelState.IsValid)
            {
                // to Add to DB

                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Category created successfull";
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

            var categortFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (categortFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categortFromDbFirst);
        }


        // Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplatOrder.ToString())
            {
                // Error place     // Msg will Display
                //ModelState.AddModelError("Customer Error", "The DisplayOrder cannot exactly match the Name");
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            // check if form have data or Not 
            if (ModelState.IsValid)
            {
                // to Update to DB

                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["success"] = "Category Updated successfull";
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

            var categortFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (categortFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categortFromDbFirst);
        }


        // Post Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }


            // to Delete to DB

            _unitofwork.Category.Remove(obj);
            _unitofwork.Save();
            TempData["success"] = "Category Deleted successfull";

            return RedirectToAction("Index");

        }

    }
}
