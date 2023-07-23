using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContex _db;

        public CategoryController(ApplicationDbContex db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // to Retrieve All Data 
            IEnumerable<Category> objCategoryList = _db.categories;
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

                _db.categories.Add(obj);
                _db.SaveChanges();
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

            var categortFromDb = _db.categories.Find(id);

            //var categortFromDbFirst = _db.categories.FirstOrDefault(u =>u.Id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (categortFromDb == null)
            {
                return NotFound();
            }
            return View(categortFromDb);
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

                _db.categories.Update(obj);
                _db.SaveChanges();
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

            var categortFromDb = _db.categories.Find(id);

            //var categortFromDbFirst = _db.categories.FirstOrDefault(u =>u.Id == id);
            //var categortFromDbSingle = _db.categories.SingleOrDefault(u =>u.Id == id);

            if (categortFromDb == null)
            {
                return NotFound();
            }
            return View(categortFromDb);
        }


        // Post Action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            
            
                // to Delete to DB

                _db.categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category Deleted successfull";

            return RedirectToAction("Index");
         
        }

    }
}
