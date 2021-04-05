using BnrCodingExercise.Controllers;
using BnrCodingExercise.Core.Entities;
using BnrCodingExercise.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BnrCodingExercise.Tests
{
    public class PostsControllerTests
    {
        [Fact]
        public void WhenNewedUp_ShouldThrowExceptionWhenAppDataContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostsController(null));
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ShouldReturnPosts()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            await ctx.Posts.AddAsync(expected);
            await ctx.SaveChangesAsync();

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.GetAll();

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeOfType<List<Post>>();

            var value = (List<Post>)objectResult.Value;

            value.Count.Should().Be(1);
            value[0].Id.Should().Be(expected.Id);
            value[0].Body.Should().Be(expected.Body);
            value[0].Title.Should().Be(expected.Title);
            value[0].UserId.Should().Be(expected.UserId);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetById_ShouldReturnBadRequestWhenIdIsDefault()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.GetById(default);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetById_ShouldReturnPost()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            await ctx.Posts.AddAsync(expected);
            await ctx.SaveChangesAsync();

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.GetById(expected.Id);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeOfType<Post>();

            var value = (Post)objectResult.Value;

            value.Id.Should().Be(expected.Id);
            value.Body.Should().Be(expected.Body);
            value.Title.Should().Be(expected.Title);
            value.UserId.Should().Be(expected.UserId);
        }
        #endregion

        #region GetByUserId
        [Fact]
        public async Task GetByUserId_ShouldReturnBadRequestWhenUserIdIsDefault()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.GetByUser(default);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetByUserId_ShouldReturnPosts()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            await ctx.Posts.AddAsync(expected);
            await ctx.SaveChangesAsync();

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.GetByUser(expected.UserId);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeOfType<List<Post>>();

            var value = (List<Post>)objectResult.Value;

            value.Count.Should().Be(1);
            value[0].Id.Should().Be(expected.Id);
            value[0].Body.Should().Be(expected.Body);
            value[0].Title.Should().Be(expected.Title);
            value[0].UserId.Should().Be(expected.UserId);
        }
        #endregion

        #region Post
        [Fact]
        public async Task Post_ShouldReturnBadRequestWhenPostIsNull()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.Post(null);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Post_ShouldCreatePost()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.Post(expected);

            // ASSERT
            result.Should().BeOfType<ObjectResult>();

            var objectResult = (ObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            objectResult.Value.Should().BeOfType<Post>();

            var value = (Post)objectResult.Value;

            value.Id.Should().Be(expected.Id);
            value.Body.Should().Be(expected.Body);
            value.Title.Should().Be(expected.Title);
            value.UserId.Should().Be(expected.UserId);
        }
        #endregion

        #region Put
        [Fact]
        public async Task Put_ShouldReturnBadRequestWhenPostIsNull()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.Put(null);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Put_ShouldUpdate()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            await ctx.Posts.AddAsync(expected);
            await ctx.SaveChangesAsync();

            var sut = new PostsController(ctx);

            // ACT
            expected.Body = "body2";

            var result = await sut.Put(expected);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objectResult.Value.Should().BeOfType<Post>();

            var value = (Post)objectResult.Value;

            value.Id.Should().Be(expected.Id);
            value.Body.Should().Be(expected.Body);
            value.Title.Should().Be(expected.Title);
            value.UserId.Should().Be(expected.UserId);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ShouldReturnBadRequestWhenPostIsNull()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.Delete(default);

            // ASSERT
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ShouldUpdate()
        {
            // ARRANGE
            using var ctx = new AppDataContext(TestHelpers.BuildDbOptions());

            var expected = new Post
            {
                Id = 1,
                Body = "body",
                Title = "title",
                UserId = 2
            };

            await ctx.Posts.AddAsync(expected);
            await ctx.SaveChangesAsync();

            // tell EF to detach entity but don't commit
            ctx.Remove(expected);

            var sut = new PostsController(ctx);

            // ACT
            var result = await sut.Delete(expected.Id);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();

            var objectResult = (OkObjectResult)result;

            objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        #endregion
    }
}
