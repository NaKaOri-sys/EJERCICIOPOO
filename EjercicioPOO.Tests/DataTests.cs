//using System.Collections.Generic;
//using EjercicioPOO.Enum;
//using NUnit.Framework;

//namespace EjercicioPOO.Tests
//{
//    [TestFixture]
//    public class DataTests
//    {
//        [TestCase]
//        public void TestResumenListaVacia()
//        {
//            Assert.AreEqual("<h1>Lista vacía de formas!</h1>",
//                FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto>(), IdiomasEnum.Castellano));
//        }

//        [TestCase]
//        public void TestResumenListaVaciaFormasEnIngles()
//        {
//            Assert.AreEqual("<h1>Empty list of shapes!</h1>",
//                FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto>(), IdiomasEnum.Ingles));
//        }

//        [TestCase]
//        public void TestResumenListaVaciaFormasEnPortugues()
//        {
//            Assert.AreEqual("<h1>Lista vazia de formas!</h1>",
//                FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto>(), IdiomasEnum.Portugues));
//        }

//        [TestCase]
//        public void TestResumenListaConUnCuadrado()
//        {
//            var cuadrado = new CuadradoDto(5);
//            var cuadrados = new List<FormaGeometricaDto> { cuadrado };

//            var resumen = FormaGeometricaDto.Imprimir(cuadrados, IdiomasEnum.Castellano);

//            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConMasCuadrados()
//        {
//            var cuadrados = new List<FormaGeometricaDto>
//            {
//                new CuadradoDto(5),
//                new CuadradoDto(1),
//                new CuadradoDto(3)
//            };

//            var resumen = FormaGeometricaDto.Imprimir(cuadrados, IdiomasEnum.Ingles);

//            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConUnCirculoPortugues()
//        {
//            var circulo = new CirculoDto(3);

//            var resumen = FormaGeometricaDto.Imprimir(new List<FormaGeometricaDto> { circulo }, IdiomasEnum.Portugues);

//            Assert.AreEqual("<h1>Relatório de formulários</h1>1 Círculo | Área 7,07 | Perímetro 9,42 <br/>TOTAL:<br/>1 formas Perímetro 9,42 Área 7,07", resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConMasCirculoPortugues()
//        {
//            var circulo = new List<FormaGeometricaDto>
//            {
//                new CirculoDto(3),
//                new CirculoDto(1),
//                new CirculoDto(4)
//            };

//            var resumen = FormaGeometricaDto.Imprimir(circulo, IdiomasEnum.Portugues);

//            Assert.AreEqual("<h1>Relatório de formulários</h1>3 Círculos | Área 20,42 | Perímetro 25,13 <br/>TOTAL:<br/>3 formas Perímetro 25,13 Área 20,42", resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConMasTiposEnPortugues()
//        {
//            var formas = new List<FormaGeometricaDto>
//            {
//                new CuadradoDto(5),
//                new CirculoDto(3),
//                new TrianguloEquilateroDto(4),
//                new CuadradoDto(2),
//                new TrianguloEquilateroDto(9),
//                new CirculoDto(2.75m),
//                new TrianguloEquilateroDto(4.2m)
//            };

//            var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Portugues);

//            Assert.AreEqual(
//                "<h1>Relatório de formulários</h1>2 Quadrados | Área 29 | Perímetro 28 <br/>2 Círculos | Área 13,01 | Perímetro 18,06 <br/>3 Triângulos | Área 49,64 | Perímetro 51,6 <br/>TOTAL:<br/>7 formas Perímetro 97,66 Área 91,65",
//                resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConMasTipos()
//        {
//            var formas = new List<FormaGeometricaDto>
//            {
//                new CuadradoDto(5),
//                new CirculoDto(3),
//                new TrianguloEquilateroDto(4),
//                new CuadradoDto(2),
//                new TrianguloEquilateroDto(9),
//                new CirculoDto(2.75m),
//                new TrianguloEquilateroDto(4.2m)
//            };

//            var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Ingles);

//            Assert.AreEqual(
//                "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 13.01 | Perimeter 18.06 <br/>3 Triangles | Area 49.64 | Perimeter 51.6 <br/>TOTAL:<br/>7 shapes Perimeter 97.66 Area 91.65",
//                resumen);
//        }

//        [TestCase]
//        public void TestResumenListaConMasTiposEnCastellano()
//        {
//            var formas = new List<FormaGeometricaDto>
//            {
//                new CuadradoDto(5),
//                new CirculoDto(3),
//                new TrianguloEquilateroDto(4),
//                new CuadradoDto(2),
//                new TrianguloEquilateroDto(9),
//                new CirculoDto(2.75m),
//                new TrianguloEquilateroDto(4.2m)
//            };

//            var resumen = FormaGeometricaDto.Imprimir(formas, IdiomasEnum.Castellano);

//            Assert.AreEqual(
//                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 Círculos | Area 13,01 | Perimetro 18,06 <br/>3 Triángulos | Area 49,64 | Perimetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 97,66 Area 91,65",
//                resumen);
//        }
//    }
//}