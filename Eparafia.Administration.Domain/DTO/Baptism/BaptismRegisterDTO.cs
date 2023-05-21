using Eparafia.Administration.Domain.Entities;
using Eparafia.Administration.Domain.Entities.BaptismEntities;
using Eparafia.Administration.Domain.ValueObjects;
using Eparafia.Domain.ValueObjects;

namespace Eparafia.Administration.Domain.DTO.Baptism;

public class BaptismRegisterDTO
{
    public ActId ActId { get; set; }
    public BaptismClientDTO Client { get; set; }
    public BaptismParentsRelationDTO Parents { get; set; }
    public BaptismGoparentDTO Godmother { get; set; }
    public BaptismGoparentDTO Godfather { get; set; }
    public SacramentalMakerDTO SacramentalMaker { get; set; }
    public DateTime DateOfSacrament { get; set; }
    public string Comments { get; set; }

    public BaptismRegister FromDtoToEntity(Guid parishId)
    {
        var id = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var fatherId = Guid.NewGuid();
        var motherId = Guid.NewGuid();
        var godmotherId = Guid.NewGuid();
        var godfatherId = Guid.NewGuid();
        var parentsId = Guid.NewGuid();
        var sacramentalMaker = Guid.NewGuid();
        var baptismRegister = new BaptismRegister()
        {
            Id = id,
            ActId = new ActId()
            {
                Id = ActId.Id
            },
            Client = new BaptismClient()
            {
                BaptismRegisterId = id,
                Surname = Client.Surname,
                FirstName = Client.FirstName,
                BirthDate = Client.BirthDate,
                BaptismDate = Client.BaptismDate,
                Id = clientId
            },
            Parents = new BaptismParentsRelation()
            {
                BaptismRegisterId = id,
                Id = parentsId,
                FatherId = fatherId,
                Father = new BaptismFather()
                {
                    Id = fatherId,
                    BaptismParentsRelationId = parentsId,
                    FirstName = Parents.Father.FirstName,
                    LastName = Parents.Father.LastName,
                    Address = new Address(
                        Parents.Father.Address.Region,
                        Parents.Father.Address.City,
                        Parents.Father.Address.Street,
                        Parents.Father.Address.BuildingNumber,
                        Parents.Father.Address.PostCode),
                    Confession = Parents.Father.Confession,
                    Job = Parents.Father.Job,
                    DateOfBirth = Parents.Father.DateOfBirth,
                    CityOfBirth = Parents.Father.CityOfBirth
                },
                MotherId = motherId,
                Mother = new BaptismMother()
                {
                    Id = motherId,
                    BaptismParentsRelationId = parentsId,
                    FirstName = Parents.Mother.FirstName,
                    LastName = Parents.Mother.LastName,
                    Address = new Address(
                        Parents.Mother.Address.Region,
                        Parents.Mother.Address.City,
                        Parents.Mother.Address.Street,
                        Parents.Mother.Address.BuildingNumber,
                        Parents.Mother.Address.PostCode),
                    Confession = Parents.Mother.Confession,
                    Job = Parents.Mother.Job,
                    DateOfBirth = Parents.Mother.DateOfBirth,
                    CityOfBirth = Parents.Mother.CityOfBirth
                },
            },
            Godfather = new BaptismGodfather()
            {
                FirstName = Godfather.FirstName,
                LastName = Godfather.LastName,
                Confession = Godfather.Confession,
                Job = Godfather.Job,
                CityOfBirth = Godfather.CityOfBirth,
                DateOfBirth = Godfather.DateOfBirth,
                BaptismRegisterId = id,
                Id = godfatherId,
                Address = new Address(
                    Godfather.Address.Region,
                    Godfather.Address.City,
                    Godfather.Address.Street,
                    Godfather.Address.BuildingNumber,
                    Godfather.Address.PostCode)
            },
            Godmother = new BaptismGodmother()
            {
                FirstName = Godmother.FirstName,
                LastName = Godmother.LastName,
                Confession = Godmother.Confession,
                Job = Godmother.Job,
                CityOfBirth = Godmother.CityOfBirth,
                DateOfBirth = Godmother.DateOfBirth,
                BaptismRegisterId = id,
                Id = godmotherId,
                Address = new Address(
                    Godmother.Address.Region,
                    Godmother.Address.City,
                    Godmother.Address.Street,
                    Godmother.Address.BuildingNumber,
                    Godmother.Address.PostCode)
            },
            SacramentalMaker = new SacramentalMaker()
            {
                Id = sacramentalMaker,
                Firstname = SacramentalMaker.FirstName,
                Lastname = SacramentalMaker.LastName,
            },
            Comments = Comments,
            DateOfSacrament = DateOfSacrament,
            ParishId = parishId,
            ClientId = clientId,
            GodfatherId = godfatherId,
            GodmotherId = godmotherId,
            ParentsId = parentsId,
            SacramentalMakerId = sacramentalMaker,
        };
        return baptismRegister;
    }
}