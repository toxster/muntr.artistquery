# Muntr.ArtistQuery

## Getting

`git clone https://github.com/toxster/Muntr.ArtistQuery.git`

`cd Muntr.ArtistQuery`

## Installing

> Prerequisites
> * .NET Core 1.0 RTM



## Developing
It's a good idea to start this in a separate window, when files change it will automatically recompile and serve up the new functionality

> `cd src\Muntr.Server && dotnet restore && dotnet watch run`

## Testing

It's a good idea to have this running in a separate window

>`cd test\Muntr.Tests && dotnet restore && dotnet watch test`
>
> Runs xUnit tests

## Deployment

> Builds a docker container for Deployment

## TODO

better test coverage
add nLog with publishing to db/logstash
automate builds of docker to Dockerhub
reduce/retarget logging when targetting production (appsettings.prod.json) 
