using AutoMapper;
using CollegeERPSystem.Services.Controllers;
using CollegeERPSystem.Services.Domain;
using CollegeERPSystem.Services.Domain.Repositories;
using CollegeERPSystem.Services.Domain.Services;
using CollegeERPSystem.Services.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace CollegeERP.Tests
{
    public class MarksControllerTests : IAsyncLifetime
    {
        private MarksService? _service;
        private MarksRepository? _repository;
        private MarksController? _controller;
        private IMapper? _mapper;
        private MarksDTO? _marks;
        private string? _id;

        public async Task InitializeAsync()
        {
            _marks = GetDTO();

            _repository = new MarksRepository(new MongoDbSettings());

            _mapper = new MapperConfiguration(config =>
                                  config.AddProfile<AutoMapperProfile>())
                                   .CreateMapper();

            _service = new MarksService(_mapper, _repository);

            _controller = new MarksController(_service);

            var result = (ObjectResult)(await _controller.CreateAsync(_marks!)).Result!;
            _id = ((MarksDTO)((Response)result.Value!).Content!).Id;
        }

        public async Task DisposeAsync()
        {
            _ = await _controller!.DeleteAsync(_id!);
        }

        [Fact]
        public async void GetAllAsync()
        {
            var response = (ObjectResult)(await _controller!.GetAllAsync(new PaginationModel()).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((IEnumerable<MarksDTO>)((Response)response.Value!).Content!).Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetByIdAsync_Does_Not_Exist()
        {
            ObjectIdGenerator rand = new ObjectIdGenerator();
            var id = rand.GenerateId(new object(), new object()).ToString();
            var response = (ObjectResult)(await _controller!.GetByIdAsync(id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((Response)response.Value!).StatusCode!.Should().Be(404);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            var response = (ObjectResult)(await _controller!.GetByIdAsync(_id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((MarksDTO)((Response)response.Value!).Content!).MarksObtained.Should().Be(_marks!.MarksObtained);
        }

        [Fact]
        public async void DeleteAsync()
        {
            MarksDTO? _marksDTODel = GetDTO();
            string id = ((MarksDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_marksDTODel!)).Result!).Value!)
                               .Content!).Id!;

            var response = (ObjectResult)(await _controller!.DeleteAsync(id).ConfigureAwait(false)).Result!;

            Assert.NotNull(response.Value);
            ((MarksDTO)((Response)response.Value!).Content!).Id.Should().Be(_marksDTODel!.Id);
        }

        [Fact]
        public async void CreateAsync()
        {
            MarksDTO? _marksDTOCre = GetDTO();
            var result = (MarksDTO)((Response)((ObjectResult)(await _controller!.CreateAsync(_marksDTOCre!)).Result!).Value!).Content!;

            Assert.NotNull(result.Percentage);
            result.MarksObtained.Should().Be(_marksDTOCre!.MarksObtained);
        }

        [Fact]
        public async void UpdateAsync()
        {
            MarksDTO? _marksDTOUpd = GetDTO();
            _marksDTOUpd!.Id = _id;
            var result = ((Response)((ObjectResult)(await _controller!.UpdateAsync(_marksDTOUpd)).Result!).Value!);

            Assert.NotNull(result);
             ((MarksDTO) result.Content!).MarksObtained.Should().Be(_marksDTOUpd!.MarksObtained);
        }

        private MarksDTO? GetDTO()
        {
            Filler<MarksDTO> _marksDTO = new();

            ObjectIdGenerator rand = new ObjectIdGenerator();
            var id = rand.GenerateId(new object(),new object()).ToString();

            _marksDTO.Setup()
                .OnProperty(x => x.Id).Use(id);
                
            return _marksDTO.Create();
        }
    }
}
