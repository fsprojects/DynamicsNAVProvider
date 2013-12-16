(*** hide ***)
#I "../../bin"

(**
Getting started with the DynamicsNAVProvider
============================================

Open Visual Studio 2013 and create a new F# Console application. Right-click on the project and select "Manage NuGet Packages".
Make sure the "Include Prerelease" is selected and search for "[DynamicsNAVProvider](https://nuget.org/packages/DynamicsNAVProvider)" and press "Install":

![alt text](img/NuGet.png "Install the Dynamics NAV type provider")


Example
-------

This example demonstrates the use of the type provider:

*)
// reference the type provider dll
#r "FSharp.Data.DynamicsNAVProvider.dll"
open System
open System.Linq
open FSharp.Data

// configure the Dynamics NAV type provider with a connection string to the db
// and set the company
type NAV = DynamicsNAV<"Data Source=OMEGA;Initial Catalog=Dev;Integrated Security=True",
                           Company="CRONUS International Ltd.">
let db = NAV.GetDataContext()

// now you have typed access to the whole Dynamics NAV database

// look into the sales headers
for sh in db.``Sales Header`` do
    printfn "%s %s" sh.``Sell-to Customer No.`` sh.``Salesperson Code``

(**

You will even get Intellisense for all of this:

![alt text](img/TypedDynamicsNAV.png "Intellisense for Dynamics NAV")

*)

// Retrieve all companies
for company in db.Company do
    printfn "%s" company.Name
// [fsi: Company 1]
// [fsi: CRONUS International Ltd.]

(**

Queries
-------

It's possible to perform LINQ queries against the Dynamics NAV database:

*)

query{ for cus in db.Customer do
       join sh in db.``Sales Header`` on (cus.``No.`` = sh.``Sell-to Customer No.``)
       select (cus.Name,sh.``No.``,sh.``Currency Code``) } 
  |> Seq.toArray

(**
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read [library design notes][readme] to understand how it works.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/forki/DynamicsNAVProvider/tree/master/docs/content
  [gh]: https://github.com/forki/DynamicsNAVProvider
  [issues]: https://github.com/forki/DynamicsNAVProvider/issues
  [readme]: https://github.com/forki/DynamicsNAVProvider/blob/master/README.md
  [license]: https://github.com/forki/DynamicsNAVProvider/blob/master/LICENSE.md
*)