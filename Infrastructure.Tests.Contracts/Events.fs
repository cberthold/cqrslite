namespace Infrastructure.Tests.Contracts.Events
open System
open Infrastructure.Events

type DomainObjectCreated = { Id:Guid; Name:string }
    with interface IEvent with member this.Id with get() = this.Id

type DomainObjectRenamed = { Id:Guid; Name:string }
    with interface IEvent with member this.Id with get() = this.Id
