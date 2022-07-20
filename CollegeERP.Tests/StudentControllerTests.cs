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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace CollegeERP.Tests
{
    public class StudentControllerTests : IAsyncLifetime
    {
        private StudentService? _service;
        private StudentRepository? _repository;
        private StudentsController? _controller;
        private IMapper? _mapper;
        private StudentDTO? _student;
        private int? _id;
        private AppDbContext? context;

        public async Task InitializeAsync()
        {
            _student = GetDTO();

            //def. of Database InMemory
            context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().EnableSensitiveDataLogging().
                            UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseInMemoryDatabase("TestData").Options);

            _repository = new StudentRepository(context);

            _mapper = new MapperConfiguration(config =>
                                  config.AddProfile<AutoMapperProfile>())
                                   .CreateMapper();

            _service = new StudentService(_mapper, _repository);

            _controller = new StudentsController(_service);

            var result = (ObjectResult)(await _controller.CreateAsync(_student!)).Result!;
            _id = ((StudentDTO)((Response)result.Value!).Content!).Id;
            context.ChangeTracker.Clear();
        }

        public async Task DisposeAsync()
        {
            _ = await _controller!.DeleteAsync(_id!.Value);
        }

        [Fact]
        public async void GetAllAsync()
        {
            var response = (ObjectResult)(await _controller!.GetAllAsync(new PaginationModel()).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((IEnumerable<StudentDTO>)((Response)response.Value!).Content!).Count().Should().BeGreaterThan(0);
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
            ((StudentDTO)((Response)response.Value!).Content!).Name.Should().Be(_student!.Name);
        }

        [Fact]
        public async void DeleteAsync()
        {
            StudentDTO? _studentDTODel = GetDTO();
            int id = ((StudentDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_studentDTODel!)).Result!).Value!)
                               .Content!).Id!.Value;
            context!.ChangeTracker.Clear();

            var response = (ObjectResult)(await _controller!.DeleteAsync(id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((StudentDTO)((Response)response.Value!).Content!).IsDeleted.Should().Be(true); ;
        }

        [Fact]
        public async void CreateAsync()
        {
            StudentDTO? _studentDTOCre = GetDTO();
            var result = (StudentDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_studentDTOCre!)).Result!).Value!).Content!;

            Assert.NotNull(result.Name);
            result.Name.Should().Be(_studentDTOCre!.Name);
        }

        [Fact]
        public async void UpdateAsync()
        {
            StudentDTO? _StudentDTOUpd = GetDTO();
            _StudentDTOUpd!.Id = _id;
            var result = ((Response)((ObjectResult)(await _controller!.UpdateAsync(_StudentDTOUpd)).Result!).Value!);

            Assert.NotNull(result);
            //   result.Code.Should().Be(_StudentDTOUpd!.Code);
        }

        private StudentDTO? GetDTO()
        {
            Filler<StudentDTO> _studentDTO = new();

            _studentDTO.Setup()
                .OnProperty(x => x.Name).Use(new MnemonicString(1, 1, Constants.NameSize))
                .OnProperty(x => x.RegistrationNo).Use(new MnemonicString(1, 1, Constants.NameSize))
                .OnProperty(x => x.IsDeleted).Use(false)
                .OnType<Programme>().IgnoreIt()
                .OnType<Class>().IgnoreIt();

            return _studentDTO.Create();
        }
    }
}