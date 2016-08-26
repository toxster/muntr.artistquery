# Muntr.ArtistQuery
Aggregate data about an artist using various 3rd party APIs.


## Obtaining

`git clone https://github.com/toxster/Muntr.ArtistQuery.git`

`cd Muntr.ArtistQuery`

## Installing

> Prerequisites
> * .NET Core 1.0 RTM
> * Docker


## Developing
It's a good idea to start this in a separate window, when files change it will automatically recompile and serve up the new functionality

> `cd src\Muntr.Server && dotnet restore && dotnet watch run`

you can now query the api at localhost:5000/api/artist/<mb-id-guid>

## Testing

>`cd test\Muntr.Tests && dotnet restore && dotnet test`
>
> Runs xUnit tests

Hint: when doing TDD:
>`cd test\Muntr.Tests && dotnet restore && dotnet watch test`

The tests will automatically run after you edit a test.

## Deployment

Since this API relies heavily on 3:rd party APIs, we put a reverse proxy (nginx) in front of it. Caching is crucial when dealing with many requests.

* Build and publish the project
>`cd src\Muntr.Server && dotnet publish`
* Install Docker for your platform (tested in Win10-x64 and MacOS 10.11)
* Build and start up the containers
> `docker-compose up --build` 
(make sure to run this in the same dir as this README.md)

you should now have 1 reverse proxy with 3 app servers running on localhost:80

## TODO

* better test coverage
* add nLog with publishing to db/logstash, forward nginx logging aswell.
* automate builds of docker to Dockerhub 
* CI scripts for automatic testing in docker container.
* add Letsencrypt SSL automatically to nginx and host over SSL/HTTP2

## Shortcuts taken
* Not creating proper classes for the various adapters, instead we cherrypick what we need from the returning JSON.
* Not throwing custom exceptions out of the adapters, instead we rely on the server returning 500 to the client and that the clients acts appropriately.
* Not testing all various errors in the 3:rd party API's (missing id, wrong format of id, etc.)

## Examples

`curl -H "application/json" http://localhost:5000/api/artist/5441c29d-3602-4898-b1a1-b77fa23b8e50`

Please note that the above command target a single app server ran using dotnet (port 5000 used)

Response:
```json    
{
  "mbid": "5441c29d-3602-4898-b1a1-b77fa23b8e50",
  "name": "David Bowie",
  "type": "Person",
  "country": "GB",
  "description": "<p><b>David Robert Jones</b> (8 January 1947 – 10 January 2016), known professionally as <b>David Bowie</b> (<span><span>/<span><span title=\"/ˈ/ primary stress follows\">ˈ</span><span title=\"'b' in 'buy'\">b</span><span title=\"/oʊ/ long 'o' in 'code'\">oʊ</span><span title=\"/i/ 'y' in 'happy'\">i</span></span>/</span></span>), was an English singer, songwriter and actor. He was a figure in popular music for over five decades, regarded by critics and musicians as an innovator, particularly for his work in the 1970s. His career was marked by reinvention and visual presentation, his music and stagecraft significantly influencing popular music. During his lifetime, his record sales, estimated at 140 million worldwide, made him one of the world's best-selling music artists. In the UK, he was awarded nine platinum album certifications, eleven gold and eight silver, releasing eleven number-one albums. In the US, he received five platinum and seven gold certifications. He was inducted into the Rock and Roll Hall of Fame in 1996.</p>\n<p>Born and raised in south London, Bowie developed an interest in music as a child, eventually studying art, music and design before embarking on a professional career as a musician in 1963. \"Space Oddity\" became his first top-five entry on the UK Singles Chart after its release in July 1969. After a period of experimentation, he re-emerged in 1972 during the glam rock era with his flamboyant and androgynous alter ego Ziggy Stardust. The character was spearheaded by the success of his single \"Starman\" and album <i>The Rise and Fall of Ziggy Stardust and the Spiders from Mars</i>, which won him widespread popularity. In 1975, Bowie's style shifted radically towards a sound he characterised as \"plastic soul\", initially alienating many of his UK devotees but garnering him his first major US crossover success with the number-one single \"Fame\" and the album <i>Young Americans</i>. In 1976, Bowie starred in the cult film <i>The Man Who Fell to Earth</i> and released <i>Station to Station</i>. The following year, he further confounded musical expectations with the electronic-inflected album <i>Low</i> (1977), the first of three collaborations with Brian Eno that would come to be known as the \"Berlin Trilogy\". <i>\"Heroes\"</i> (1977) and <i>Lodger</i> (1979) followed; each album reached the UK top five and received lasting critical praise.</p>\n<p>After uneven commercial success in the late 1970s, Bowie had UK number ones with the 1980 single \"Ashes to Ashes\", its parent album <i>Scary Monsters (And Super Creeps)</i>, and \"Under Pressure\", a 1981 collaboration with Queen. He then reached his commercial peak in 1983 with <i>Let's Dance</i>, with its title track topping both UK and US charts. Throughout the 1990s and 2000s, Bowie continued to experiment with musical styles, including industrial and jungle. Bowie also continued acting; his roles included Major Celliers in <i>Merry Christmas, Mr. Lawrence</i> (1983), the Goblin King Jareth in <i>Labyrinth</i> (1986), Pontius Pilate in <i>The Last Temptation of Christ</i> (1988), and Nikola Tesla in <i>The Prestige</i> (2006), among other film and television appearances and cameos. He stopped concert touring after 2004 and his last live performance was at a charity event in 2006. In 2013, Bowie returned from a decade-long recording hiatus with the release of <i>The Next Day</i> and remained musically active until he died of liver cancer two days after the release of his final album, <i>Blackstar</i> (2016).</p>\n<p></p>",
  "albums": [
    {
      "title": "Diamond Dogs",
      "id": "0dc4835d-b21a-3612-bac6-ab1e782a1396",
      "image": "http://coverartarchive.org/release/06a786fe-75dc-42ee-9c91-47d2739f77fc/5478339046.jpg",
      "thumbsmall": "http://coverartarchive.org/release/06a786fe-75dc-42ee-9c91-47d2739f77fc/5478339046-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/06a786fe-75dc-42ee-9c91-47d2739f77fc/5478339046-500.jpg"
    },
    {
      "title": "Reality",
      "id": "0f9d1be4-e975-39e9-a5ee-867c87824f42",
      "image": "http://coverartarchive.org/release/ead3a08a-3c12-4172-87ea-9d0d12ac59d5/12892322385.jpg",
      "thumbsmall": "http://coverartarchive.org/release/ead3a08a-3c12-4172-87ea-9d0d12ac59d5/12892322385-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/ead3a08a-3c12-4172-87ea-9d0d12ac59d5/12892322385-500.jpg"
    },
    {
      "title": "“Heroes”",
      "id": "1f5ef8d3-10ca-30eb-b41e-85b16987d412",
      "image": "http://coverartarchive.org/release/f0a4ed57-10e0-4c37-81b4-36311dc7d4b6/5087053841.jpg",
      "thumbsmall": "http://coverartarchive.org/release/f0a4ed57-10e0-4c37-81b4-36311dc7d4b6/5087053841-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/f0a4ed57-10e0-4c37-81b4-36311dc7d4b6/5087053841-500.jpg"
    },
    {
      "title": "The Man Who Sold the World",
      "id": "2536a41d-fde9-35d5-a6c6-cd4d94ffd916",
      "image": "http://coverartarchive.org/release/8784de2f-5754-3bcf-8b2a-35d327ef74a1/9074463656.jpg",
      "thumbsmall": "http://coverartarchive.org/release/8784de2f-5754-3bcf-8b2a-35d327ef74a1/9074463656-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/8784de2f-5754-3bcf-8b2a-35d327ef74a1/9074463656-500.jpg"
    },
    {
      "title": "Never Let Me Down",
      "id": "28f8a0f8-0eb9-35a2-b6cd-d5be21e4428c",
      "image": "http://coverartarchive.org/release/84213f7b-950e-4a2f-9ea9-a51b898c22ee/1706737302.jpg",
      "thumbsmall": "http://coverartarchive.org/release/84213f7b-950e-4a2f-9ea9-a51b898c22ee/1706737302-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/84213f7b-950e-4a2f-9ea9-a51b898c22ee/1706737302-500.jpg"
    },
    {
      "title": "Tonight",
      "id": "2d0af79a-cb12-32e3-9cae-21696b910d53",
      "image": "http://coverartarchive.org/release/0ef585d7-049a-4967-be97-e58b7be08f9b/12497655677.jpg",
      "thumbsmall": "http://coverartarchive.org/release/0ef585d7-049a-4967-be97-e58b7be08f9b/12497655677-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/0ef585d7-049a-4967-be97-e58b7be08f9b/12497655677-500.jpg"
    },
    {
      "title": "David Bowie",
      "id": "2e12918c-4973-3537-b9ab-e4723ae1ae1d",
      "image": "http://coverartarchive.org/release/767ee61d-6395-4ba9-bf79-ab0bf6701669/3315265976.jpg",
      "thumbsmall": "http://coverartarchive.org/release/767ee61d-6395-4ba9-bf79-ab0bf6701669/3315265976-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/767ee61d-6395-4ba9-bf79-ab0bf6701669/3315265976-500.jpg"
    },
    {
      "title": "Aladdin Sane",
      "id": "50f8710f-3ae6-319b-85a7-afe783f13449",
      "image": "http://coverartarchive.org/release/6e1e07e6-1d9d-4c09-bef5-0a81d89df7f9/6901891780.jpg",
      "thumbsmall": "http://coverartarchive.org/release/6e1e07e6-1d9d-4c09-bef5-0a81d89df7f9/6901891780-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/6e1e07e6-1d9d-4c09-bef5-0a81d89df7f9/6901891780-500.jpg"
    },
    {
      "title": "1990-04-03: Paris Au Printemps",
      "id": "515d7971-9d5c-4fbc-8602-20eab39b4334",
      "image": null,
      "thumbsmall": null,
      "thumblarge": null
    },
    {
      "title": "David Bowie",
      "id": "5db40351-97ed-36a8-88e2-a21a2603cae1",
      "image": "http://coverartarchive.org/release/9d6d8272-55fd-4b7b-a71e-4d9df6ee8f00/10232448309.jpg",
      "thumbsmall": "http://coverartarchive.org/release/9d6d8272-55fd-4b7b-a71e-4d9df6ee8f00/10232448309-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/9d6d8272-55fd-4b7b-a71e-4d9df6ee8f00/10232448309-500.jpg"
    },
    {
      "title": "Station to Station",
      "id": "686cdbd1-b400-32d8-8cb8-9317a2e41a58",
      "image": "http://coverartarchive.org/release/6cd16d0d-4269-4cd2-ac9a-067d746d5c94/5086932734.jpg",
      "thumbsmall": "http://coverartarchive.org/release/6cd16d0d-4269-4cd2-ac9a-067d746d5c94/5086932734-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/6cd16d0d-4269-4cd2-ac9a-067d746d5c94/5086932734-500.jpg"
    },
    {
      "title": "The Rise and Fall of Ziggy Stardust and the Spiders From Mars",
      "id": "6c9ae3dd-32ad-472c-96be-69d0a3536261",
      "image": "http://coverartarchive.org/release/bb7f0f5a-586e-3f90-8665-fdc547aa2a54/1239299887.jpg",
      "thumbsmall": "http://coverartarchive.org/release/bb7f0f5a-586e-3f90-8665-fdc547aa2a54/1239299887-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/bb7f0f5a-586e-3f90-8665-fdc547aa2a54/1239299887-500.jpg"
    },
    {
      "title": "Hunky Dory",
      "id": "743b0b2e-a23a-3182-950e-232f8cb0dfb7",
      "image": "http://coverartarchive.org/release/98d6d2b9-d173-3313-a5d9-c740753c1a66/1951275113.jpg",
      "thumbsmall": "http://coverartarchive.org/release/98d6d2b9-d173-3313-a5d9-c740753c1a66/1951275113-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/98d6d2b9-d173-3313-a5d9-c740753c1a66/1951275113-500.jpg"
    },
    {
      "title": "Let’s Dance",
      "id": "75c2bddf-1799-3eda-b6b3-a0cf5189d8ed",
      "image": "http://coverartarchive.org/release/a9102496-59fa-39a1-9fbc-bcc1332211e3/9504569986.jpg",
      "thumbsmall": "http://coverartarchive.org/release/a9102496-59fa-39a1-9fbc-bcc1332211e3/9504569986-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/a9102496-59fa-39a1-9fbc-bcc1332211e3/9504569986-500.jpg"
    },
    {
      "title": "Pin Ups",
      "id": "8b7bd1c2-be07-3083-989a-714f219f1ff8",
      "image": "http://coverartarchive.org/release/4abe64de-327b-37fd-a233-c9843f70c868/2997775097.jpg",
      "thumbsmall": "http://coverartarchive.org/release/4abe64de-327b-37fd-a233-c9843f70c868/2997775097-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/4abe64de-327b-37fd-a233-c9843f70c868/2997775097-500.jpg"
    },
    {
      "title": "Young Americans",
      "id": "8c2a0eae-1359-3577-9127-e3d862acc2a2",
      "image": "http://coverartarchive.org/release/afe8516a-dc2e-42b0-9cc0-ff9c0f21c338/5478263268.jpg",
      "thumbsmall": "http://coverartarchive.org/release/afe8516a-dc2e-42b0-9cc0-ff9c0f21c338/5478263268-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/afe8516a-dc2e-42b0-9cc0-ff9c0f21c338/5478263268-500.jpg"
    },
    {
      "title": "Earthling",
      "id": "9a723e6e-2ede-371b-88bf-396cc362d77b",
      "image": "http://coverartarchive.org/release/fcbb4e5c-9701-4aec-96ae-13160d9c8631/4036440196.jpg",
      "thumbsmall": "http://coverartarchive.org/release/fcbb4e5c-9701-4aec-96ae-13160d9c8631/4036440196-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/fcbb4e5c-9701-4aec-96ae-13160d9c8631/4036440196-500.jpg"
    },
    {
      "title": "Scary Monsters… and Super Creeps",
      "id": "bb13cb45-254c-3a61-89a5-15d22a97e6d6",
      "image": "http://coverartarchive.org/release/104f475e-1761-4228-b9e4-ed46d086848a/3791440379.jpg",
      "thumbsmall": "http://coverartarchive.org/release/104f475e-1761-4228-b9e4-ed46d086848a/3791440379-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/104f475e-1761-4228-b9e4-ed46d086848a/3791440379-500.jpg"
    },
    {
      "title": "Lodger",
      "id": "d6dd84a2-1a62-378c-b6c9-86a8b2211634",
      "image": "http://coverartarchive.org/release/abcd7321-97a9-4d0a-93c8-b2067403d970/12784958539.jpg",
      "thumbsmall": "http://coverartarchive.org/release/abcd7321-97a9-4d0a-93c8-b2067403d970/12784958539-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/abcd7321-97a9-4d0a-93c8-b2067403d970/12784958539-500.jpg"
    },
    {
      "title": "On Air",
      "id": "e61c281f-c170-422d-a380-4e83e2bafe21",
      "image": "http://coverartarchive.org/release/d162ee0c-48ed-40c2-9f63-93a719d7ca81/10919824651.jpg",
      "thumbsmall": "http://coverartarchive.org/release/d162ee0c-48ed-40c2-9f63-93a719d7ca81/10919824651-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/d162ee0c-48ed-40c2-9f63-93a719d7ca81/10919824651-500.jpg"
    },
    {
      "title": "‘hours…’",
      "id": "e8a94ea7-8466-389e-830a-d356801ca68c",
      "image": "http://coverartarchive.org/release/99eb4d59-f74c-3dad-ab63-92f2064d0a54/3040642228.jpg",
      "thumbsmall": "http://coverartarchive.org/release/99eb4d59-f74c-3dad-ab63-92f2064d0a54/3040642228-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/99eb4d59-f74c-3dad-ab63-92f2064d0a54/3040642228-500.jpg"
    },
    {
      "title": "1.Outside: The Nathan Adler Diaries: A Hyper Cycle",
      "id": "f05647bf-86a7-3e6e-872a-20eb465be0a5",
      "image": "http://coverartarchive.org/release/85060db3-1f2c-3b37-8200-cfb792e1b0db/11795714892.jpg",
      "thumbsmall": "http://coverartarchive.org/release/85060db3-1f2c-3b37-8200-cfb792e1b0db/11795714892-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/85060db3-1f2c-3b37-8200-cfb792e1b0db/11795714892-500.jpg"
    },
    {
      "title": "Black Tie White Noise",
      "id": "f5cc15ea-67a8-3c6b-9887-d45ae13a3156",
      "image": "http://coverartarchive.org/release/fce82de3-d8c3-4bb7-93f0-fc2ac32ff03f/2852146170.jpg",
      "thumbsmall": "http://coverartarchive.org/release/fce82de3-d8c3-4bb7-93f0-fc2ac32ff03f/2852146170-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/fce82de3-d8c3-4bb7-93f0-fc2ac32ff03f/2852146170-500.jpg"
    },
    {
      "title": "Low",
      "id": "f6a51281-56c4-3538-b915-65a9d4eb29b5",
      "image": "http://coverartarchive.org/release/83e786b5-d49c-4efd-bd10-57914f2eec9b/9838985185.jpg",
      "thumbsmall": "http://coverartarchive.org/release/83e786b5-d49c-4efd-bd10-57914f2eec9b/9838985185-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/83e786b5-d49c-4efd-bd10-57914f2eec9b/9838985185-500.jpg"
    },
    {
      "title": "Heathen",
      "id": "fed49206-4869-307d-9bfa-08109424cb73",
      "image": "http://coverartarchive.org/release/9df4f306-75b0-4ab6-bc99-ad0dccdd81c3/6624908579.jpg",
      "thumbsmall": "http://coverartarchive.org/release/9df4f306-75b0-4ab6-bc99-ad0dccdd81c3/6624908579-250.jpg",
      "thumblarge": "http://coverartarchive.org/release/9df4f306-75b0-4ab6-bc99-ad0dccdd81c3/6624908579-500.jpg"
    }
  ]
}
```
