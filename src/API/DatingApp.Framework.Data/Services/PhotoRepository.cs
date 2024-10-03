using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Interfaces;
using DatingApp.Framework.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Services
{
    public class PhotoRepository(DataContext dbContext, ILoggerFactory logger) : Repository<Photo>(dbContext, logger), IPhotoRepository
    {
        
    }
}
