namespace Infrastructure.Tests.Contracts.Commands
open System
open Infrastructure.Commands

// domainObject commands
type CreateDomainObject = { Id: Guid; Name: string } 
    with interface ICommand
type RenameDomainObject = { Id: Guid; Name: string }
    with interface ICommand