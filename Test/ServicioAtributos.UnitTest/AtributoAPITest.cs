using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CRUDBasico.Controllers;
using CRUDBasico.Infrastructure;
using CRUDBasico.Infrastructure.BD.Repository;
using CRUDBasico.Infrastructure.Specification;
using CRUDBasico.Model;
using CRUDBasico.UnitTest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace CRUDBasico.UnitTests
{
    public class AtributoAPITest
    {
        private readonly Mock<ILogger<AtributosController>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IAtributosSpecification> _specification;
        private readonly Mock<IAtributosRepository> _repo;

        public AtributoAPITest()
        {
            _logger = new Mock<ILogger<AtributosController>>();
            _mapper = new Mock<IMapper>();
            _specification = new Mock<IAtributosSpecification>();
            _repo = new Mock<IAtributosRepository>();
        }

        [Theory]
        [InlineData(6)]
        public async Task Remove_requestId_success(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                           .Returns(FakeAtributo);

            //Act
            var atributosController = new AtributosController(_logger.Object,_specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (OkObjectResult)await atributosController.DeleteAtributo(id);
            
            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(6)]
        public async Task Remove_requestId_not_found(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(500);

            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                           .Returns(FakeAtributo);
            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (NotFoundResult)await atributosController.DeleteAtributo(id);

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Remove_not_ID_not_found()
        {
            //Arrange

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (NotFoundResult)await atributosController.DeleteAtributo(0);

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_All_Atributos()
        {
            //Arrange
            List<Atributo> FakeAtributos = new List<Atributo>();
            FakeAtributos.Add(Build.CrearAtributo(1));
            List<AtributoDto> FakeDtos = Build.CrearAtributosDto();

            _repo.Setup(repor => repor.GetElements()).Returns(FakeAtributos);
            _mapper.Setup(map => map.Map<List<Atributo>, List<AtributoDto>>(It.IsAny<List<Atributo>>())).Returns(FakeDtos);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = await Task.Run(() => (OkObjectResult)atributosController.GetAtributos());

            List<AtributoDto> resultado = (List<AtributoDto>)actionResult.Value;
            //Assert
            Assert.Equal(resultado.Count, FakeDtos.Count);
            Assert.Equal(resultado.FirstOrDefault(), FakeDtos.FirstOrDefault());
        }

        [Theory]
        [InlineData(6)]
        public async Task Get_requestId_Atributos(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            AtributoDto FakeDto = Build.CrearAtributoDto(id);

            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                           .Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<Atributo, AtributoDto>(It.IsAny<Atributo>())).Returns(FakeDto);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = await Task.Run(() => (OkObjectResult)atributosController.GetAtributoID(id));

            AtributoDto resultado = (AtributoDto)actionResult.Value;
            
            //Assert
            Assert.Equal(resultado, FakeDto);
        }

        [Theory]
        [InlineData(6)]
        public async Task Get_requestId_Atributos_not_found(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(20);
            AtributoDto FakeDto = Build.CrearAtributoDto(20);

            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                           .Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<Atributo, AtributoDto>(It.IsAny<Atributo>())).Returns(FakeDto);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = await Task.Run(() => (NotFoundResult)atributosController.GetAtributoID(id));

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(6)]
        public async Task Create_Atributo(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            AtributoDto FakeDto = Build.CrearAtributoDto(id);
            AtributoDto FakeRequestDto = Build.CrearAtributoDto(id);

           
            _mapper.Setup(map => map.Map<AtributoDto, Atributo>(It.IsAny<AtributoDto>())).Returns(FakeAtributo);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (CreatedResult)await atributosController.RegisterAtributo(FakeRequestDto);

            AtributoDto resultado = (AtributoDto)actionResult.Value;

            //Assert
            Assert.Equal(resultado, FakeRequestDto);
        }

        [Theory]
        [InlineData(6)]
        public async Task Create_Atributo_Repeated(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            AtributoDto FakeDto = Build.CrearAtributoDto(id);
            AtributoDto FakeRequestDto = Build.CrearAtributoDto(id);

            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                          .Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<AtributoDto, Atributo>(It.IsAny<AtributoDto>())).Returns(FakeAtributo);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (StatusCodeResult)await atributosController.RegisterAtributo(FakeRequestDto);

            //Assert
            Assert.Equal(actionResult.StatusCode, StatusCodes.Status500InternalServerError);
        }


        [Theory]
        [InlineData(6)]
        public async Task Modify_Atributo(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            AtributoDto FakeDto = Build.CrearAtributoDto(id);
            AtributoDto FakeRequestDto = Build.CrearAtributoDto(id);

            _repo.Setup(repor => repor.GetElement(It.IsAny<ISpecification<Atributo>>()))
                           .Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<AtributoDto, Atributo>(It.IsAny<AtributoDto>())).Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<Atributo, AtributoDto>(It.IsAny<Atributo>())).Returns(FakeDto);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (OkObjectResult)await atributosController.ModificarAtributo(FakeRequestDto);

            AtributoDto resultado = (AtributoDto)actionResult.Value;

            //Assert
            Assert.Equal(resultado, FakeDto);
        }

        [Theory]
        [InlineData(6)]
        public async Task Modify_Atributo_Not_Exist(int id)
        {
            //Arrange
            Atributo FakeAtributo = Build.CrearAtributo(id);
            AtributoDto FakeDto = Build.CrearAtributoDto(id);
            AtributoDto FakeRequestDto = Build.CrearAtributoDto(id);

            _mapper.Setup(map => map.Map<AtributoDto, Atributo>(It.IsAny<AtributoDto>())).Returns(FakeAtributo);
            _mapper.Setup(map => map.Map<Atributo, AtributoDto>(It.IsAny<Atributo>())).Returns(FakeDto);

            //Act
            var atributosController = new AtributosController(_logger.Object, _specification.Object, _repo.Object, _mapper.Object);
            var actionResult = (NotFoundResult)await atributosController.ModificarAtributo(FakeRequestDto);

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }
    }
}

