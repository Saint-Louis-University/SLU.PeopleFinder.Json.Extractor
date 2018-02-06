# SLU.PeopleFinder.Json.Extractor

Class library written in C# to extract specific datum from PeopleFinder queries.

**Goal** 

Make it easier to extract specific datum from PeopleFinder queries.

**Technology Overview**

Request JSON results from PeopleFinder and parse results using Json.NET

**Prerequisites**

* .NET Framwork 4.6.1
* [Json.NET v10.0.3](https://www.newtonsoft.com/json)

## Getting Started

1. Install Visual Studio 2017
2. Install [Json.NET](https://www.newtonsoft.com/json) via NuGet: Install-Package Newtonsoft.Json -Version 10.0.3
3. Pull repository
4. Build
5. Import into your project

## Resources

* PeopleFinder Json Url: https://www.slu.edu/peoplefinder/json/json_index.php?q=<query>
* [Json.NET documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm)

## Notes

Currently written as a functional service. That is, one must query PeopleFinder for every datum.

In the future, may convert over to storing the result-set locally so that it might be further processed.

## Authors / Owners

[Matt Schuelke](mailto:"matthew.schuelke@slu.edu")
