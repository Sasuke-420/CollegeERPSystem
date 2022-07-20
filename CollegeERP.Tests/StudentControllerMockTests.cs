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
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace CollegeERP.Tests
{
    public class StudentControllerMockTests
    {
        private StudentsController _controller;
        private Mock<StudentService> _service;
        private StudentRepository _repo;
        private StudentDTO? _student;
        private IMapper? mapper;
        private AppDbContext context;

        public StudentControllerMockTests()
        {
            context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().EnableSensitiveDataLogging().
                      UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseInMemoryDatabase("TestData").Options);
            mapper = new MapperConfiguration(x=>x.AddProfile<AutoMapperProfile>()).CreateMapper();
            _repo = new StudentRepository(context);
            _service = new Mock<StudentService>(mapper,_repo);
            _controller = new StudentsController(_service.Object);
            _student = GetDTO();
        }


        [Fact]
        public async void GetAllAsync()
        {
            var _record = GetDTO();
            List<StudentDTO> _students = new();
            _students.Add(_record!);
            _students.Add(_student!);

            var response = new Response(null, true, 200,null, _students.AsEnumerable());
             _service.Setup(x => x.GetAllAsync(It.IsAny<PaginationModel>())).ReturnsAsync(response);

            var result = (ObjectResult)(await _controller!.GetAllAsync(new PaginationModel()).ConfigureAwait(false)).Result!;

            Assert.NotNull(result.Value);
            ((IEnumerable<StudentDTO>)((Response)result.Value!).Content!).Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetByIdAsync_Does_Not_Exist()
        {
            var response = new Response(null, true, 404, null, null);

            _service.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(response);
            var result = (ObjectResult)(await _controller!.GetByIdAsync(-1).ConfigureAwait(false)).Result!;

            response.StatusCode.Should().Be(404);
            response.Content!.Should().Be(null);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            var response = new Response(null, true, 200, null, _student);

            _service.Setup(x => x.GetByIdAsync(_student!.Id!.Value)).ReturnsAsync(response);
            var result = (ObjectResult)(await _controller!.GetByIdAsync(_student!.Id).ConfigureAwait(false)).Result!;

            Assert.NotNull(result.Value);
            ((StudentDTO)((Response)result.Value!).Content!).Name.Should().Be(_student!.Name);
        }

        [Fact]
        public async void DeleteAsync()
        {
            StudentDTO? _studentDTODel = GetDTO();
            _studentDTODel!.IsDeleted = true;
            var response = new Response(null, true, 202, null, _studentDTODel);

            _service.Setup(x => x.GetByIdAsync(_studentDTODel!.Id!.Value)).ReturnsAsync(response);
            _service.Setup(x => x.DeleteAsync(_studentDTODel!.Id!.Value)).ReturnsAsync(response);

            var result = (ObjectResult)(await _controller!.DeleteAsync(_studentDTODel!.Id!.Value).ConfigureAwait(false)).Result!;

            ((Response)result.Value!).StatusCode.Should().Be(202);
            ((StudentDTO)((Response)result.Value!).Content!).IsDeleted.Should().Be(true); ;
        }

        [Fact]
        public async void CreateAsync()
        {
            StudentDTO? _studentDTOCre = GetDTO();
            var response = new Response(null, true, 202, null, _studentDTOCre);

            _service.Setup(x => x.CreateAsync(_studentDTOCre!)).ReturnsAsync(response);
            var result = (StudentDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_studentDTOCre!)).Result!).Value!).Content!;

            Assert.NotNull(result.Name);
            result.Name.Should().Be(_studentDTOCre!.Name);
        }

        [Fact]
        public async void UpdateAsync()
        {
            StudentDTO? _StudentDTOUpd = GetDTO();
            _StudentDTOUpd!.Id = _student!.Id;
            var response = new Response(null, true, 202, null, _StudentDTOUpd);

            _service.Setup(x => x.UpdateAsync(_StudentDTOUpd!)).ReturnsAsync(response);
            var result = ((Response)((ObjectResult)(await _controller!.UpdateAsync(_StudentDTOUpd)).Result!).Value!);

            Assert.NotNull(result);
              ((StudentDTO)result.Content!).ClassId.Should().Be(_StudentDTOUpd!.ClassId);
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
