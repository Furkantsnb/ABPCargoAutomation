
using CargoAutomation.Agentas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace CargoAutomation
{
    public class CagoAutomationDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;

        public CagoAutomationDataSeederContributor(IRepository<Agenta, Guid> agentaRepository,IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> transferCenterRepository)
        {
            _agentaRepository = agentaRepository;
            _transferCenterRepository = transferCenterRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _agentaRepository.GetCountAsync() > 0)
            {
                return;
            }

            var antalyaTransferCenter = await _transferCenterRepository.InsertAsync(
                new CargoAutomation.TransferCenters.TransferCenter
                {
                    
                    UnitName = "Antalya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Antalya",
                    District = "kepez",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false
                });

            var malatyaTransferCenter = await _transferCenterRepository.InsertAsync(
                new CargoAutomation.TransferCenters.TransferCenter
                {
                  
                    UnitName = "Malatya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Malatya",
                    District = "Malatya",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false
                });

            var elazigTransferCenter = await _transferCenterRepository.InsertAsync(
                new CargoAutomation.TransferCenters.TransferCenter
                {
                    
                    UnitName = "Elazığ",
                    ManagerName = "Arif",
                    ManagerSurname = "Arif",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Elazığ",
                    District = "Elazığ",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false
                });

            await _agentaRepository.InsertAsync(
                new Agenta
                {
                    
                    UnitName = "Antalya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Antalya",
                    District = "kepez",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false,
                  
                },
                autoSave: true
            );

            await _agentaRepository.InsertAsync(
                new Agenta
                {
                    
                    UnitName = "Malatya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Malatya",
                    District = "Malatya",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false,
                   
                },
                autoSave: true
            );

            await _agentaRepository.InsertAsync(
                new Agenta
                {
                    
                    UnitName = "Elazığ",
                    ManagerName = "Arif",
                    ManagerSurname = "Arif",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Elazığ",
                    District = "Elazığ",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false,
                 
                },
                autoSave: true
            );
        }

    }
}
