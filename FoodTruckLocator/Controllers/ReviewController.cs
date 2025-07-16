using AutoMapper;
using FoodTruckLocator.Dtos;
using FoodTruckLocator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FoodTruckLocator.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FoodTruckLocator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // Get all reviews for a specific food truck
        [HttpGet("truck/{truckId}")]
        public async Task<IActionResult> GetByTruck(int truckId)
        {
            var allReviews = await _reviewService.GetAllAsync();

            var filteredReviews = allReviews.Where(r => r.TruckID == truckId).ToList();

            return Ok(_mapper.Map<List<ReviewDto>>(filteredReviews));
        }

        // Only customers can create reviews
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var review = _mapper.Map<Review>(dto);
            review.UserID = userId;
            review.CreatedAt = DateTime.UtcNow;

            await _reviewService.AddAsync(review);

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        // Customers can update their own reviews, Admin can update any review
        [Authorize(Roles = "Customer,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateReviewDto dto)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            bool isAdmin = roles.Contains("Admin");

            if (!isAdmin && review.UserID != userId)
                return Forbid();

            _mapper.Map(dto, review);
            await _reviewService.UpdateAsync(review);

            return NoContent();
        }

        // Customers can delete their own reviews, Admin can delete any review
        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            bool isAdmin = roles.Contains("Admin");

            if (!isAdmin && review.UserID != userId)
                return Forbid();

            await _reviewService.DeleteAsync(id);

            return NoContent();
        }
    }
}
