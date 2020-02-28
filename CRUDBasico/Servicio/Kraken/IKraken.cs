using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRUDBasico.Servicio.Kraken
{
    /// <summary>
    /// Interfaz servicio kraken
    /// </summary>
    public interface IKraken
    {
        /// <summary>
        /// Crear mensaje necesario para enviar al Kraken
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="operation"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        string CreateMessageKraken<T>(string target, string operation, List<T> data);

        /// <summary>
        /// Enviar el mensaje al kraken por Http POST
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<string> SendKrakenPOST(string message);
    }
}
