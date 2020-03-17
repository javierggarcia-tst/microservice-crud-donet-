using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using System;
using System.Collections.Generic;
using CRUDBasico.FunctionalTests;
using CRUDBasico.Model;

namespace CRUDBasico.FunctionalTests
{
    public class ServicioLiquidacionScenarioFuncional : ServicioLiquidacionScenarioBase
    {
       
        [Theory]
        [InlineData(6)]
        public async Task Get_get_id_stored_atributos_and_response_ok_status_code(int id)
        {
            try
            {
                var server = CreateServerFunctional();
                var host = await server.StartAsync();
                var response = await host.GetTestClient().GetAsync(Get.AtributosBy(id));
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();

                string atributoTeorico = generarAtributo();

                // Assert
                Assert.Equal(atributoTeorico, responseString);

            }catch(Exception e)
            {
                throw e;
            }
        }

        [Fact]
        public async Task Get_get_all_stored_atributos_and_response_ok_status_code()
        {
            try
            {
                var server = CreateServerFunctional();
                var host = await server.StartAsync();
                var response = await host.GetTestClient().GetAsync(Get.Atributos);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                List<AtributoDto> listaAtributos = JsonConvert.DeserializeObject<List<AtributoDto>>(responseString);
                
                Assert.True(listaAtributos.Count > 0);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string generarAtributo()
        {
            List<AtributoDto> atributos = new List<AtributoDto>();
            
            AtributoDto atributo = new AtributoDto();
            atributo.id = 6;
            atributo.descripcion = "Media terminal individual recaudado";
            atributos.Add(atributo);

            return JsonConvert.SerializeObject(atributo);
        }
    }
}
