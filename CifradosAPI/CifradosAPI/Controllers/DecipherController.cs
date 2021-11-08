using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Encryptor;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecipherController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult postdecipher([FromForm] IFormFile[] files, [FromForm] string key)
        {
            byte[] buffer = new byte[0];
            byte[] bufferfinal = new byte[0];

            Encryptator descifrador = null;

            IFormFile file = files[0];
            string nombrearchiv = file.FileName;
            string nombrearchivofinal = nombrearchiv.Split(".").First();

            if (file == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            
            else if (Path.GetExtension(file.FileName) == ".zz")
            {
                int llave = Convert.ToInt32(key);
                descifrador = new ZigZagEncryptator();
                using (MemoryStream archivotexto = new MemoryStream())

                    try
                    {

                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = descifrador.decrypt(archivotexto.ToArray(), llave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".txt");
                        FileContentResult fileToReturn = File(bufferfinal, "text/plane");
                        fileToReturn.FileDownloadName = nombrearchivofinal + ".txt";
                        return fileToReturn;
                    }
                    catch (Exception)
                    {
                        return StatusCode(500);
                    }

            }
            else if (Path.GetExtension(file.FileName) == ".csr")
            {
                byte[] bytellave = Encoding.ASCII.GetBytes(key);
                descifrador = new CesarEncrypt();
                using (MemoryStream archivotexto = new MemoryStream())

                    try
                    {

                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = descifrador.decrypt(archivotexto.ToArray(), bytellave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".txt");
                        FileContentResult fileToReturn = File(bufferfinal, "text/plane");
                        fileToReturn.FileDownloadName = nombrearchivofinal + ".txt";
                        return fileToReturn;
                    }
                    catch (Exception)
                    {
                        return StatusCode(500);
                    }

            }
            else if (Path.GetExtension(file.FileName) == ".rt")
            {
                int llave = Convert.ToInt32(key);
                descifrador = new RutaEncryptator();
                using (MemoryStream archivotexto = new MemoryStream())

                    try
                    {
                        var bytesarray = archivotexto.ToArray();
                        file.CopyToAsync(archivotexto);
                        bufferfinal = descifrador.decrypt(archivotexto.ToArray(), llave);
                        generarArchivo_txt(bufferfinal, nombrearchivofinal, ".txt");
                        FileContentResult fileToReturn = File(bufferfinal, "text/plane");
                        fileToReturn.FileDownloadName = nombrearchivofinal + ".txt";
                        return fileToReturn;
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500);
                    }
            }
            return Ok();
        }

        public void generarArchivo_txt(byte[] datos, string name, string extension)
        {
            string fileName = "../../Archivos/" + name + extension;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {

                fileStream.Write(datos, 0, datos.Length);
            }

        }
    }
}