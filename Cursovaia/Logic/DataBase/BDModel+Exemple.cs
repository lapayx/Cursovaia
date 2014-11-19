using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

namespace MedalForHeroes.Logic.Database
{
    public partial class Entities : IImageRepository
    {
        /// <summary>
        /// Изображения.
        /// </summary>
        IDbSet<Image> IImageRepository.Images
        {
            get { return Images; }
        }

        /// <summary>
        /// Заявки.
        /// </summary>
        IDbSet<ImageRequest> IImageRepository.ImageRequests
        {
            get { return ImageRequests; }
        }
    }
}
