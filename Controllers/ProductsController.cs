
//Libraries
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

//My Collections
using mongonetAPI.Context;
using mongonetAPI.Models;

namespace mongonetApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private Context _context;
        public ProductsController() => _context = new Context();

        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            try
            {           
                var results = await _context.Products.FindAsync(_ => true);
                var list = await results.ToListAsync();
                if(list.Count() > 0 ) return Ok(list);
                return  Ok(new {message = "No products in database."});
            }
            catch (Exception ex)
            {
                //so em abiente de desenvolviento...    
                return Ok(new {errorMessage = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {   
                if( String.IsNullOrEmpty(product.name) || 
                    String.IsNullOrEmpty(product.Description)) 
                        throw new Exception("parsing wrong params");
                var result = _context.Products.InsertOneAsync(product);
                return Accepted(product);
            }
            catch (Exception ex)
            {     
                //so em abiente de desenvolviento...    
                return Ok(new {errorMessage = ex.Message});
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product product)
        {
            try
            {
                if( String.IsNullOrEmpty(product.name) || 
                    String.IsNullOrEmpty(product.Description)) 
                        throw new Exception("parsing wrong params");
                var filter = Builders<Product>
                .Filter
                .Eq(f => f.id, id);

                var update = Builders<Product>.Update
                .Set(set => set.name, product.name)
                .Set(set => set.Description, product.Description);

                var result = await _context.Products.UpdateOneAsync(filter, update);
                
                if(result.ModifiedCount > 0){
                    product.id = id;
                    return Accepted(product);
                }else return Ok(new { message = "Product not found."});

            }
            catch (Exception ex)
            {                
                //so em abiente de desenvolviento...
                return Ok(new {errorMessage = ex.Message});
            }
        }
        [HttpDelete("{id}")]
        public async void Delete(string id){
            try
            {
                var filter =  Builders<Product>.Filter
                .Eq(filter => filter.id, id);
                await _context.Products.DeleteOneAsync(filter);
            }catch(Exception){}
        }

    }
}


