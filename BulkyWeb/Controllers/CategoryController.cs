using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order and Name should not be same");
            }
            //if (obj.Name!=null && obj.Name == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }
        public IActionResult Editt(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            //Three ways to pass id. Find works only on PK of table
            //Category? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            Category? categoryFromDb1 = _db.Categories.Find(id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();


                if (categoryFromDb1 == null) { return NotFound(); }
            return View(categoryFromDb1);

        }
        [HttpPost]
        public IActionResult Editt(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            //Three ways to pass id. Find works only on PK of table
            //Category? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            Category? categoryFromDb1 = _db.Categories.Find(id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();


            if (categoryFromDb1 == null) { return NotFound(); }
            return View(categoryFromDb1);

        }
        [HttpPost, ActionName("Delete")]
        
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);
            if(obj == null) { return NotFound(); }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index", "Category");
            

            

        }
    }
}
