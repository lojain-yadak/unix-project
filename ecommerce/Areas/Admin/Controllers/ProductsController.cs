using Microsoft.AspNetCore.Mvc;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            //var products = context.Products.Join(context.Categories, p => p.CategoryId, c => c.Id, (p, c) => new
            //{
            //    p.Id,
            //    p.Name,
            //    p.Description,
            //    p.Price,
            //    p.Image,
            //    categoryName=c.Name
            //});
            var products2 = context.Products.Include(p => p.Category).ToList();
            var productsVm = new List<ProductsViewModel>();
            foreach (var item in products2)
            {
                var vm = new ProductsViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{item.Image}",
                    CategoryName = item.Category.Name
                };
                productsVm.Add(vm);
            }
            return View(productsVm);
        }

        public IActionResult Create()
        {
            ViewBag.categories = context.Categories.ToList();
            return View(new Product());
        }
        //[ValidateAntiForgeryToken]
        public IActionResult Store(Product request, IFormFile file)
        {
            ViewBag.categories = context.Categories.ToList();
            ModelState.Remove("File");
            if (!ModelState.IsValid)
            {
                return View("Create",request);
            }

            if (file == null || file.Length == 0) {
                ModelState.AddModelError("Image", "please upload an image");
                return View("Create", request);

            }
            var allowedExtensions = new[] { ".jpg", ".webp" };
            var extension= Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension)) {
                ModelState.AddModelError("Image", "only jpg or webp are allowed");
                return View("Create", request);
            }

            if (file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "image size must be less than 2MB");
                return View("Create", request);


            }
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filepath))
                {
                    file.CopyTo(stream);
                }
                request.Image = fileName;
                context.Products.Add(request);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var product = context.Products.Find(id);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
            System.IO.File.Delete(filepath);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(product);
        }

        public IActionResult Update(/*int id,*/ Product request, IFormFile? file)
        {
            var product = context.Products.Find(request.Id);
            product.Name =request.Name;
            product.Description =request.Description;
            product.Price =request.Price;
            product.Quantity =request.Quantity;
            product.CategoryId = request.CategoryId;

            if (file != null && file.Length > 0) {

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
                System.IO.File.Delete(oldFilePath);

                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filepath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
                
            }
        

            request.Image = product.Image;
            //context.Products.Update(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
