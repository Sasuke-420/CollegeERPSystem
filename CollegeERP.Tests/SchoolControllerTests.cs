using AutoMapper;
using CollegeERPSystem.Services.Controllers;
using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Models;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tynamix.ObjectFiller;

namespace CollegeERP.Tests
{
    public class SchoolControllerTests : IAsyncLifetime
    {
        private SchoolService? _service;
        private SchoolRepository? _repository;
        private SchoolsController? _controller;
        private IMapper? _mapper;
        private SchoolDTO? _school;
        private int? _id;
        private AppDbContext? context;

        public async Task InitializeAsync()
        {
            _school = GetDTO();

            //def. of Database InMemory
            context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().EnableSensitiveDataLogging().
                            UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseInMemoryDatabase("TestData").Options);

            _repository = new SchoolRepository(context);

            _mapper = new MapperConfiguration(config =>
                                  config.AddProfile<AutoMapperProfile>())
                                   .CreateMapper();

            _service = new SchoolService(_mapper, _repository);

            _controller = new SchoolsController(_service);

            var result = (ObjectResult)(await _controller.CreateAsync(_school!)).Result!;
            _id = ((SchoolDTO)((Response)result.Value!).Content!).Id;
            context.ChangeTracker.Clear();
        }

        public async Task DisposeAsync()
        {
            _ = await _controller!.DeleteAsync(_id!.Value);
        }

        [Fact]
        public async void GetAllAsync()
        {
            var response = (ObjectResult)(await _controller!.GetAllAsync().ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((IEnumerable<SchoolDTO>)((Response)response.Value!).Content!).Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetByIdAsync_Does_Not_Exist()
        {
            var response = (ObjectResult)(await _controller!.GetByIdAsync(-1).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((Response)response.Value!).StatusCode!.Should().Be(404);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            var response = (ObjectResult)(await _controller!.GetByIdAsync(_id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((SchoolDTO)((Response)response.Value!).Content!).Code.Should().Be(_school!.Code);
        }

        [Fact]
        public async void DeleteAsync()
        {
            SchoolDTO? _schoolDTODel = GetDTO();
            int id = ((SchoolDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_schoolDTODel!)).Result!).Value!)
                               .Content!).Id!.Value;
            context!.ChangeTracker.Clear();

            var response = (ObjectResult)(await _controller!.DeleteAsync(id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((SchoolDTO)((Response)response.Value!).Content!).IsDeleted.Should().Be(true);
        }

        [Fact]
        public async void CreateAsync()
        {
            SchoolDTO? _schoolDTOCre = GetDTO();
            var result = (SchoolDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_schoolDTOCre!)).Result!).Value!).Content!;

            Assert.NotNull(result.Code);
            result.Code.Should().Be(_schoolDTOCre!.Code);
        }

        [Fact]
        public async void UpdateAsync()
        {
            SchoolDTO? _schoolDTOUpd = GetDTO();
            _schoolDTOUpd!.Id = _id;
            var result = ((Response)((ObjectResult)(await _controller!.UpdateAsync(_schoolDTOUpd)).Result!).Value!);

            Assert.NotNull(result);
            //   result.Code.Should().Be(_schoolDTOUpd!.Code);
        }

        private SchoolDTO? GetDTO()
        {
            Filler<SchoolDTO> _schoolDto = new();

            _schoolDto.Setup()
                .OnProperty(x => x.Name).Use(new MnemonicString(1, 1, Constants.NameSize))
                .OnProperty(x => x.Code).Use(new MnemonicString(1, 1, Constants.CodeSize))
                .OnProperty(x => x.IsDeleted).Use(false)
                .OnType<Org>().IgnoreIt();

            return _schoolDto.Create();
        }
    }
}