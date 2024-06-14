using CargoAutomation.Agentas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace CargoAutomation
{
    public class CargoAutomationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public CargoAutomationDataSeedContributor(
            IRepository<Agenta, Guid> agentaRepository,
            IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> transferCenterRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _agentaRepository = agentaRepository;
            _transferCenterRepository = transferCenterRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _agentaRepository.GetCountAsync() > 0)
                {
                    return;
                }

                var antalyaTransferCenter = new CargoAutomation.TransferCenters.TransferCenter
                {
                     CreatorId=_guidGenerator.Create(),
                    UnitName = "Antalya",
                    ManagerName = "Furkan",
                    ManagerSurname = "Taşan",
                    PhoneNumber = "123123123",
                    Gsm = "123123",
                    Email = "furkantsn@gmail.com",
                    Description = "açıklama",
                    City = "Antalya",
                    District = "Kepez",
                    NeighbourHood = "Güneş Mh.",
                    Street = "6033sk.",
                    AddressDetail = "Adres Detay",
                    IsDeleted = false
                };
                await _transferCenterRepository.InsertAsync(antalyaTransferCenter, autoSave: true);

                var malatyaTransferCenter = new CargoAutomation.TransferCenters.TransferCenter
                {
                    CreatorId = _guidGenerator.Create(),
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
                };
                await _transferCenterRepository.InsertAsync(malatyaTransferCenter, autoSave: true);

                var elazigTransferCenter = new CargoAutomation.TransferCenters.TransferCenter
                {
                    CreatorId = _guidGenerator.Create(),
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
                };
                await _transferCenterRepository.InsertAsync(elazigTransferCenter, autoSave: true);

                await _agentaRepository.InsertAsync(
                    new Agenta
                    {
                        CreatorId = _guidGenerator.Create(),
                        UnitName = "Antalya",
                        ManagerName = "Furkan",
                        ManagerSurname = "Taşan",
                        PhoneNumber = "123123123",
                        Gsm = "123123",
                        Email = "furkantsn@gmail.com",
                        Description = "açıklama",
                        City = "Antalya",
                        District = "Kepez",
                        NeighbourHood = "Güneş Mh.",
                        Street = "6033sk.",
                        AddressDetail = "Adres Detay",
                        IsDeleted = false
                    },
                    autoSave: true
                );

                await _agentaRepository.InsertAsync(
                    new Agenta
                    {
                        CreatorId = _guidGenerator.Create(),
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
                    },
                    autoSave: true
                );

                await _agentaRepository.InsertAsync(
                    new Agenta
                    {
                        CreatorId = _guidGenerator.Create(),
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
                    },
                    autoSave: true
                );
            }
        }
    }
}
