using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISmsRepository
    {
        public void AddSmsMaster(SmsMaster smsMaster);
        public void AddSmsDetail(SmsDetail smsDetail);
        public Task<bool> Complete();
    }
}
