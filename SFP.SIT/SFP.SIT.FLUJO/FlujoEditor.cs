using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SFP.SIT.FLUJO
{
    public class FlujoEditor
    {



        static void Main()
        { }

        public FlujoEditor() { }

        public void createViews(string name)
        {
            string folderName = @"c:\Top-Level Folder";
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string pathString1 = System.IO.Path.Combine(startupPath, "Views");
            string pathString = System.IO.Path.Combine(pathString1, "Respuesta");


            //            SubFolder
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create. 
            string fileName = "PV"+name;

            pathString = System.IO.Path.Combine(pathString, fileName);


            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!File.Exists(pathString))
            {
                // Create a file to write to.

                String myvar = "@{" +
                "    Layout = \"~/Views/Shared/_LayoutBase.cshtml\";" +
                "}" + "\n" +
                "" + "\n" +
                "<div id=\"divMain\" style=\"padding-left:10px; margin-top:2px; margin-bottom:2px;\">" + "\n" +
                "    <div class=\"w2ui-field w2ui-span6\" style=\"clear: both\">" + "\n" +
                "        <label>Acción a realizar :</label>" + "\n" +
                "        <div>" + "\n" +
                "            <select id=\"lstAccion\" style=\"float: left; width:450px;\"> </select>" + "\n" +
                "        </div>" + "\n" +
                "    </div>" + "\n" +
                "" + "\n" +
                "    <div class=\"w2ui-field w2ui-span6\" style=\"clear: both\">" + "\n" +
                "        <label>Instrucciones :</label>" + "\n" +
                "        <div>" + "\n" +
                "            <div id=\"divInstr\" style=\"background-color:white; height:65px; width:99%; overflow:auto; margin-top:5px; margin-left:5px; display:inline-block\">" + "\n" +
                "                @Html.Raw(ViewBag.Instruccion)" + "\n" +
                "            </div>" + "\n" +
                "        </div>" + "\n" +
                "    </div>" + "\n" +
                "</div>" + "\n" +
                "" + "\n" +
                "<div id=\"layoutTopMain\" style=\"height:99%;\"> </div>" + "\n" +
                "" + "\n" +
                "@section Scripts" + "\n" +
                "    {" + "\n" +
                "    <script>" + "\n" +
                "    var _PaginaActual;" + "\n" +
                "" + "\n" +
                "    _ListaAccion = @Html.Raw(ViewBag.ListaAccion);" + "\n" +
                "" + "\n" +
                "    $(function () {" + "\n" +
                "        w2ui['layoutMain'].content('main'," + "\n" +
                "            {" + "\n" +
                "                render: function ()" + "\n" +
                "                {" + "\n" +
                "                    $(this.box).append($('#layoutTopMain').detach());" + "\n" +
                "                    $('#layoutTopMain').w2layout(GetLayoutTopMain(\"130px\", \"70%\"));" + "\n" +
                "" + "\n" +
                "                    @*--PANEL TOP -- *@" + "\n" +
                "                    w2ui['layoutTopMain'].content('top', {" + "\n" +
                "                        render: function () {" + "\n" +
                "                            $(this.box).append($('#divMain').detach());" + "\n" +
                "                            SelectAddElemClearLimite(\"lstAccion\", _ListaAccion, _solfecsol, _FechaAct, 0);" + "\n" +
                "                        }" + "\n" +
                "                    });" + "\n" +
                "" + "\n" +
                "                    $('#lstAccion').change(function () {" + "\n" +
                "                        if ($('#lstAccion').val() != null) {" + "\n" +
                "                            _PaginaActual = _UrlRespuesta + AristaClick(_ListaAccion, parseInt($('#lstAccion').val())) + \"?solclave=\" + _solclave + \"&proclave=\" + _proclave + \"&nodclave=\" + _nodclave;" + "\n" +
                "" + "\n" +
                "                            AjaxValidarSesion(_UrlSesionActiva, _UrlInicio);" + "\n" +
                "" + "\n" +
                "                            w2ui['layoutTopMain'].load('main', _PaginaActual, null, function () {" + "\n" +
                "                                $(\"#btnAccion\").remove();" + "\n" +
                "                                $('#divMenuAccion').append(\"<a id='btnAccion' name='btnAccion'> <div class='boton2'><div class='num4'></div></div> </a> \");" + "\n" +
                "                                Inicializar();" + "\n" +
                "                                $('#btnAccion').click(Grabar);" + "\n" +
                "                                $('#avanzar').val(1);" + "\n" +
                "                            });" + "\n" +
                "                        }" + "\n" +
                "                    });" + "\n" +
                "" + "\n" +
                "                }" + "\n" +
                "            }" + "\n" +
                "        );" + "\n" +
                "    });" + "\n" +
                "    </script>" + "\n" +
                "}";



                string createText = myvar + Environment.NewLine;
                File.WriteAllText(pathString, createText);
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
        }

        public void createClasses(string name)
        {

            string folderName = @"c:\Top-Level Folder";
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string pathString1 = System.IO.Path.Combine(startupPath, "Views");
            string pathString = System.IO.Path.Combine(pathString1, "Estado");


            //            SubFolder
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create. 
            string fileName = name;

            pathString = System.IO.Path.Combine(pathString, fileName);


            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!File.Exists(pathString))
            {
                // Create a file to write to.

                string myvar = "@{" +
                "    Layout = \"~/Views/Shared/_LayoutBase.cshtml\";" +
                "}" + "\n" +
                "" + "\n" +
                "<div id=\"divMain\" style=\"padding-left:10px; margin-top:2px; margin-bottom:2px;\">" + "\n" +
                "    <div class=\"w2ui-field w2ui-span6\" style=\"clear: both\">" + "\n" +
                "        <label>Acción a realizar :</label>" + "\n" +
                "        <div>" + "\n" +
                "            <select id=\"lstAccion\" style=\"float: left; width:450px;\"> </select>" + "\n" +
                "        </div>" + "\n" +
                "    </div>" + "\n" +
                "" + "\n" +
                "    <div class=\"w2ui-field w2ui-span6\" style=\"clear: both\">" + "\n" +
                "        <label>Instrucciones :</label>" + "\n" +
                "        <div>" + "\n" +
                "            <div id=\"divInstr\" style=\"background-color:white; height:65px; width:99%; overflow:auto; margin-top:5px; margin-left:5px; display:inline-block\">" + "\n" +
                "                @Html.Raw(ViewBag.Instruccion)" + "\n" +
                "            </div>" + "\n" +
                "        </div>" + "\n" +
                "    </div>" + "\n" +
                "</div>" + "\n" +
                "" + "\n" +
                "<div id=\"layoutTopMain\" style=\"height:99%;\"> </div>" + "\n" +
                "" + "\n" +
                "@section Scripts" + "\n" +
                "    {" + "\n" +
                "    <script>" + "\n" +
                "    var _PaginaActual;" + "\n" +
                "" + "\n" +
                "    _ListaAccion = @Html.Raw(ViewBag.ListaAccion);" + "\n" +
                "" + "\n" +
                "    $(function () {" + "\n" +
                "        w2ui['layoutMain'].content('main'," + "\n" +
                "            {" + "\n" +
                "                render: function ()" + "\n" +
                "                {" + "\n" +
                "                    $(this.box).append($('#layoutTopMain').detach());" + "\n" +
                "                    $('#layoutTopMain').w2layout(GetLayoutTopMain(\"130px\", \"70%\"));" + "\n" +
                "" + "\n" +
                "                    @*--PANEL TOP -- *@" + "\n" +
                "                    w2ui['layoutTopMain'].content('top', {" + "\n" +
                "                        render: function () {" + "\n" +
                "                            $(this.box).append($('#divMain').detach());" + "\n" +
                "                            SelectAddElemClearLimite(\"lstAccion\", _ListaAccion, _solfecsol, _FechaAct, 0);" + "\n" +
                "                        }" + "\n" +
                "                    });" + "\n" +
                "" + "\n" +
                "                    $('#lstAccion').change(function () {" + "\n" +
                "                        if ($('#lstAccion').val() != null) {" + "\n" +
                "                            _PaginaActual = _UrlRespuesta + AristaClick(_ListaAccion, parseInt($('#lstAccion').val())) + \"?solclave=\" + _solclave + \"&proclave=\" + _proclave + \"&nodclave=\" + _nodclave;" + "\n" +
                "" + "\n" +
                "                            AjaxValidarSesion(_UrlSesionActiva, _UrlInicio);" + "\n" +
                "" + "\n" +
                "                            w2ui['layoutTopMain'].load('main', _PaginaActual, null, function () {" + "\n" +
                "                                $(\"#btnAccion\").remove();" + "\n" +
                "                                $('#divMenuAccion').append(\"<a id='btnAccion' name='btnAccion'> <div class='boton2'><div class='num4'></div></div> </a> \");" + "\n" +
                "                                Inicializar();" + "\n" +
                "                                $('#btnAccion').click(Grabar);" + "\n" +
                "                                $('#avanzar').val(1);" + "\n" +
                "                            });" + "\n" +
                "                        }" + "\n" +
                "                    });" + "\n" +
                "" + "\n" +
                "                }" + "\n" +
                "            }" + "\n" +
                "        );" + "\n" +
                "    });" + "\n" +
                "    </script>" + "\n" +
                "}";


                string createText = myvar + Environment.NewLine;
                File.WriteAllText(pathString, createText);
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }


        }
      
    }
}
