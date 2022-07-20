using AutoMapper;
using CollegeERPSystem.Services.Controllers;
using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace CollegeERP.Tests
{
    public class MarksControllerMockTests
    {
        private MarksController _controller;
        private Mock<MarksService> _service;
        private MarksRepository _repo;
        private MarksDTO? _marks;
        private IMapper? mapper;

        public MarksControllerMockTests()
        {

            mapper = new MapperConfiguration(x => x.AddProfile<AutoMapperProfile>()).CreateMapper();
            _repo = new MarksRepository(new MongoDbSettings());
            _service = new Mock<MarksService>(mapper, _repo);
            _controller = new MarksController(_service.Object);
            _marks = GetDTO();
        }


        [Fact]
        public async void GetAllAsync()
        {
            var _record = GetDTO();
            List<MarksDTO> _markss = new();
            _markss.Add(_record!);
            _markss.Add(_marks!);

            var response = new Response(null, true, 200, null, _markss.AsEnumerable());
            _service.Setup(x => x.GetAllAsync(It.IsAny<PaginationModel>())).ReturnsAsync(response);

            var result = (ObjectResult)(await _controller!.GetAllAsync(new PaginationModel()).ConfigureAwait(false)).Result!;

            Assert.NotNull(result.Value);
            ((IEnumerable<MarksDTO>)((Response)result.Value!).Content!).Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetByIdAsync_Does_Not_Exist()
        {
            var response = new Response(null, true, 404, null, null);

            _service.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(response);
            var result = (ObjectResult)(await _controller!.GetByIdAsync("sdk").ConfigureAwait(false)).Result!;

            response.StatusCode.Should().Be(404);
            response.Content!.Should().Be(null);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            var response = new Response(null, true, 200, null, _marks);

            _service.Setup(x => x.GetByIdAsync(_marks!.Id!)).ReturnsAsync(response);
            var result = (ObjectResult)(await _controller!.GetByIdAsync(_marks!.Id).ConfigureAwait(false)).Result!;

            Assert.NotNull(result.Value);
            ((MarksDTO)((Response)result.Value!).Content!).MarksObtained.Should().Be(_marks!.MarksObtained);
        }

        [Fact]
        public async void DeleteAsync()
        {
            MarksDTO? _marksDTODel = GetDTO();
            var response = new Response(null, true, 202, null, _marksDTODel);

            _service.Setup(x => x.GetByIdAsync(_marksDTODel!.Id!)).ReturnsAsync(response);
            _service.Setup(x => x.DeleteAsync(_marksDTODel!.Id!)).ReturnsAsync(response);

            var result = (ObjectResult)(await _controller!.DeleteAsync(_marksDTODel!.Id!).ConfigureAwait(false)).Result!;

            ((Response)result.Value!).StatusCode.Should().Be(202);
            ((MarksDTO)((Response)result.Value!).Content!).MarksObtained.Should().Be(_marksDTODel.MarksObtained); ;
        }

        [Fact]
        public async void CreateAsync()
        {
            MarksDTO? _marksDTOCre = GetDTO();
            var response = new Response(null, true, 202, null, _marksDTOCre);

            _service.Setup(x => x.CreateAsync(_marksDTOCre!)).ReturnsAsync(response);
            var result = (MarksDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_marksDTOCre!)).Result!).Value!).Content!;

            Assert.NotNull(result.MarksObtained);
            result.MarksObtained.Should().Be(_marksDTOCre!.MarksObtained);
        }

        [Fact]
        public async void UpdateAsync()
        {
            MarksDTO? _MarksDTOUpd = GetDTO();
            _MarksDTOUpd!.Id = _marks!.Id;
            var response = new Response(null, true, 202, null, _MarksDTOUpd);

            _service.Setup(x => x.UpdateAsync(_MarksDTOUpd!)).ReturnsAsync(response);
            var result = ((Response)((ObjectResult)(await _controller!.UpdateAsync(_MarksDTOUpd)).Result!).Value!);

            Assert.NotNull(result);
            ((MarksDTO)result.Content!).MarksObtained.Should().Be(_MarksDTOUpd!.MarksObtained);
        }


        private MarksDTO? GetDTO()
        {
            Filler<MarksDTO> _marksDTO = new();
            return _marksDTO.Create();
        }
    }
}
