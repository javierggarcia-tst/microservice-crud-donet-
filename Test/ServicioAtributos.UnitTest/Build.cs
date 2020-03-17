using CRUDBasico.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDBasico.UnitTest
{
    class Build
    {
        public static Atributo CrearAtributo(int id)
        {
            Atributo atr = new Atributo();
            atr.atributoId = id;
            atr.descripcion = "fakeAtributo";

            return atr;
        }

        public static AtributoDto CrearAtributoDto(int id)
        {
            AtributoDto atr = new AtributoDto();
            atr.id = id;
            atr.descripcion = "fakeAtributo";

            return atr;
        }

        public static List<AtributoDto> CrearAtributosDto()
        {
            List<AtributoDto> dtos = new List<AtributoDto>();
            AtributoDto atributo = new AtributoDto();
            atributo.id = 1;
            atributo.descripcion = "fakeAtributo";
            dtos.Add(atributo);
            return dtos;
        }
    }
}
