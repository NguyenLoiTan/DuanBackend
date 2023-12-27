// AdminController.cs
using AdvancedEshop.Web.API.Models;
using AdvancedEshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedEshop.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _clientFactory;

        public AdminController(IOrderRepository orderRepository, IHttpClientFactory clientFactory)
        {
            _orderRepository = orderRepository;
            _clientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var allOrders = await _orderRepository.Orders.ToListAsync();
            var unshippedOrders = allOrders.Where(o => !o.Shipped);
            var shippedOrders = allOrders.Where(o => o.Shipped);

            var viewModel = new AdminOrdersViewModel
            {
                AllOrders = allOrders,
                UnshippedOrders = unshippedOrders,
                ShippedOrders = shippedOrders
            };

            return View(viewModel);
        }

        public IActionResult ShipOrder(int id)
        {
            UpdateOrder(id, true);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResetOrder(int id)
        {
            UpdateOrder(id, false);
            return RedirectToAction(nameof(Index));
        }

        private void UpdateOrder(int id, bool shipValue)
        {
            Order? o = _orderRepository.Orders.FirstOrDefault(o => o.OrderID == id);
            if (o != null)
            {
                o.Shipped = shipValue;
                _orderRepository.SaveOrder(o);
            }
        }

        /*public async Task<IActionResult> ProductAll()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/products2");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            return View("ProductAll", products);
        }*/
        public async Task<IActionResult> ProductAll()
        {
            var client = _clientFactory.CreateClient();

            // Fetch products
            var productResponse = await client.GetAsync("https://localhost:7136/products2");
            productResponse.EnsureSuccessStatusCode();
            var productContent = await productResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(productContent);

            // Fetch categories
            var categoriesResponse = await client.GetAsync("https://localhost:7136/categories2");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);

            // Fetch colors
            var colorsResponse = await client.GetAsync("https://localhost:7136/colors2");
            colorsResponse.EnsureSuccessStatusCode();
            var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
            var colors = JsonConvert.DeserializeObject<List<Color>>(colorsContent);

            // Fetch sizes
            var sizesResponse = await client.GetAsync("https://localhost:7136/sizes2");
            sizesResponse.EnsureSuccessStatusCode();
            var sizesContent = await sizesResponse.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(sizesContent);

            var result = from product in products
                         join category in categories on product.CategoryId equals category.CategoryId
                         join color in colors on product.ColorId equals color.ColorId into colorGroup
                         from selectedColor in colorGroup.DefaultIfEmpty()
                         join size in sizes on product.SizeId equals size.SizeId into sizeGroup
                         from selectedSize in sizeGroup.DefaultIfEmpty()
                         select new ProductViewModel
                         {
                             ProductId = product.ProductId,
                             ProductName = product.ProductName,
                             CategoryName = category.CategoryName,
                             ProductPrice = (decimal)product.ProductPrice,
                             ProductDiscount = product.ProductDiscount,
                             ProductPhoto = product.ProductPhoto,
                             SizeId = product.SizeId,
                             ColorId = product.ColorId,
                             IsTrandy = product.IsTrandy,
                             IsArrived = product.IsArrived,
                             ColorName = selectedColor?.ColorName,
                             SizeName = selectedSize?.SizeName
                         };

            return View("ProductAll", result.ToList());
        }




        /*public async Task<IActionResult> ProductDetails(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/products2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            return View("ProductDetails", product);
        }*/

        public async Task<IActionResult> ProductDetails(int id)
        {
            var client = _clientFactory.CreateClient();

            // Fetch product details
            var productResponse = await client.GetAsync($"https://localhost:7136/products2/{id}");
            productResponse.EnsureSuccessStatusCode();
            var productContent = await productResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(productContent);

            // Fetch category details
            var categoryResponse = await client.GetAsync($"https://localhost:7136/categories2/{product.CategoryId}");
            categoryResponse.EnsureSuccessStatusCode();
            var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(categoryContent);

            // Fetch color details
            var colorResponse = await client.GetAsync($"https://localhost:7136/colors2/{product.ColorId}");
            colorResponse.EnsureSuccessStatusCode();
            var colorContent = await colorResponse.Content.ReadAsStringAsync();
            var color = JsonConvert.DeserializeObject<Color>(colorContent);

            // Fetch size details
            var sizeResponse = await client.GetAsync($"https://localhost:7136/sizes2/{product.SizeId}");
            sizeResponse.EnsureSuccessStatusCode();
            var sizeContent = await sizeResponse.Content.ReadAsStringAsync();
            var size = JsonConvert.DeserializeObject<Size>(sizeContent);

            // Create a ViewModel with additional details
            var productDetailsViewModel = new ProductDetailsViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductDiscount = product.ProductDiscount,
                ProductPhoto = product.ProductPhoto,
                IsTrandy = product.IsTrandy,
                IsArrived = product.IsArrived,
                CategoryName = category?.CategoryName,
                ColorName = color?.ColorName,
                SizeName = size?.SizeName
            };

            return View("ProductDetails", productDetailsViewModel);
        }


        public async Task<IActionResult> CreateProduct()
        {
            var client = _clientFactory.CreateClient();

            // Lấy danh sách categories
            var categoriesResponse = await client.GetAsync("https://localhost:7136/Categories2");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);

            // Lấy danh sách sizes
            var sizesResponse = await client.GetAsync("https://localhost:7136/Sizes2");
            sizesResponse.EnsureSuccessStatusCode();
            var sizesContent = await sizesResponse.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(sizesContent);

            // Lấy danh sách colors
            var colorsResponse = await client.GetAsync("https://localhost:7136/Colors2");
            colorsResponse.EnsureSuccessStatusCode();
            var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
            var colors = JsonConvert.DeserializeObject<List<Color>>(colorsContent);

            // Gán danh sách categories, sizes, và colors vào ViewBag để sử dụng trong view
            ViewBag.Categories = categories ?? new List<Category>();
            ViewBag.Sizes = sizes ?? new List<Size>();
            ViewBag.Colors = colors ?? new List<Color>();

            return View("CreateProduct");
        }

        public async Task<IActionResult> SaveProductAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                using var client = _clientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7136/products2", product);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ProductAll));
                }
                else
                {
                    // Xử lý lỗi nếu có
                    ModelState.AddModelError(string.Empty, "Error creating product. Please try again.");
                }
            }

            // Trong trường hợp có lỗi, bạn cũng cần lấy lại danh sách categories, sizes, và colors để hiển thị lại trên view
            var clientForErrors = _clientFactory.CreateClient();

            var categoriesResponse = await clientForErrors.GetAsync("https://localhost:7136/Categories2");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);

            var sizesResponse = await clientForErrors.GetAsync("https://localhost:7136/Sizes2");
            sizesResponse.EnsureSuccessStatusCode();
            var sizesContent = await sizesResponse.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(sizesContent);

            var colorsResponse = await clientForErrors.GetAsync("https://localhost:7136/Colors2");
            colorsResponse.EnsureSuccessStatusCode();
            var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
            var colors = JsonConvert.DeserializeObject<List<Color>>(colorsContent);

            // Gán lại danh sách categories, sizes, và colors vào ViewBag để sử dụng trong view
            ViewBag.Categories = categories ?? new List<Category>();
            ViewBag.Sizes = sizes ?? new List<Size>();
            ViewBag.Colors = colors ?? new List<Color>();

            return View("CreateProduct", product);
        }



        public async Task<IActionResult> EditProduct(int id)
        {
            var client = _clientFactory.CreateClient();

            // Lấy thông tin sản phẩm
            var productResponse = await client.GetAsync($"https://localhost:7136/Products2/{id}");
            productResponse.EnsureSuccessStatusCode();
            var productContent = await productResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(productContent);

            // Lấy danh sách categories
            var categoriesResponse = await client.GetAsync("https://localhost:7136/Categories2");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);

            // Lấy danh sách sizes
            var sizesResponse = await client.GetAsync("https://localhost:7136/Sizes2");
            sizesResponse.EnsureSuccessStatusCode();
            var sizesContent = await sizesResponse.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(sizesContent);

            // Lấy danh sách colors
            var colorsResponse = await client.GetAsync("https://localhost:7136/Colors2");
            colorsResponse.EnsureSuccessStatusCode();
            var colorsContent = await colorsResponse.Content.ReadAsStringAsync();
            var colors = JsonConvert.DeserializeObject<List<Color>>(colorsContent);

            // Gán danh sách categories, sizes, và colors vào ViewBag để sử dụng trong view
            ViewBag.Categories = categories ?? new List<Category>();
            ViewBag.Sizes = sizes ?? new List<Size>();
            ViewBag.Colors = colors ?? new List<Color>();

            return View("EditProduct", product);
        }

        // AdminController.cs
        /*[HttpPost]*/
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                using var client = _clientFactory.CreateClient();

                // Chú ý: Cần kiểm tra xem product.ProductId đã được đặt giá trị hay chưa.
                if (product.ProductId == 0)
                {
                    ModelState.AddModelError(string.Empty, "Invalid product ID.");
                    return View("EditProduct", product);
                }

                var response = await client.PutAsJsonAsync($"https://localhost:7136/products2/{product.ProductId}", product);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ProductAll));
                }
                else
                {
                    // Xử lý lỗi nếu có
                    ModelState.AddModelError(string.Empty, "Error updating product. Please try again.");
                }
            }

            // Nếu ModelState không hợp lệ, quay lại trang chỉnh sửa với dữ liệu người dùng đã nhập
            return View("EditProduct", product);
        }

        // AdminController.cs
        /*[HttpPost]*/
        // AdminController.cs
        public async Task<IActionResult> DeleteProduct(int id)
        {
            using var client = _clientFactory.CreateClient();

            var response = await client.DeleteAsync($"https://localhost:7136/products2/RemoveConfirmed/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(ProductAll));
            }
            else
            {
                // Xử lý lỗi nếu có
                TempData["ErrorMessage"] = "Error deleting product. Please try again.";
                return RedirectToAction(nameof(ProductAll));
            }
        }



    }

    
}
