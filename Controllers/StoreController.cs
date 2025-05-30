using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.DTO.Store;
using bike_store_2.Entities;
using bike_store_2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {

        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromForm] CreateStoreDTO store)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdStore = await _storeRepository.CreateStoreAsync(store);
                if (createdStore == null)
                    return BadRequest("Failed to create store.");

                return Ok(createdStore);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the store: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStore(int id, [FromForm] UpdateStoreDto updateStore)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedStore = await _storeRepository.UpdateStoreAsync(id, updateStore);
                if (updatedStore != null)
                    return Ok(updatedStore);

                return NotFound($"No store found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the store: {ex.Message}");
            }
        }






        [Authorize(Roles = "User")]
        [HttpGet("All Existing Stores")]
        public async Task<IActionResult> GetAllExistingStores()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stores = await _storeRepository.GetAllExistingStoresAsync();
                if (stores != null)
                    return Ok(stores);

                return NotFound("No existing stores found.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the stores: {ex.Message}");
            }
        }




        [Authorize(Roles = "Admin")]
        [HttpGet("All Deleted Stores")]
        public async Task<IActionResult> GetAllDeletedStores()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stores = await _storeRepository.GetAllDeletedStoresAsync();
                if (stores != null)
                    return Ok(stores);

                return NotFound("No deleted stores found.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the stores: {ex.Message}");
            }
        }





        [Authorize(Roles = "User")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var store = await _storeRepository.GetStoreByIdAsync(id);
                if (store != null)
                    return Ok(store);

                return NotFound($"No store found with ID {id}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the store: {ex.Message}");
            }
        }





        [Authorize(Roles = "User")]
        [HttpGet("Get Store By Name")]
        public async Task<IActionResult> GetStoreByName(string name)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var store = await _storeRepository.GetStoreByNameAsync(name);
                if (store != null)
                    return Ok(store);

                return NotFound($"No store found with name {name}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the store: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Add Product To Store")]
        public async Task<IActionResult> AddProductToStore(int id, AddProductToStoreDto addProductToStore)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _storeRepository.AddProductToStoreAsync(id, addProductToStore);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the product to the store: {ex.Message}");
            }
        }





        [Authorize(Roles = "Admin")]
        [HttpDelete("{DeleteStoreId}")]
        public async Task<IActionResult> DeleteStore(int DeleteStoreId, int alternativeStoreId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _storeRepository.DeleteStoreAsync(DeleteStoreId, alternativeStoreId);
                return Ok("Store deleted successfully.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the store: {ex.Message}");
            }
        }


    }
}
