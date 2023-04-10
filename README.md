# gosharper
Collect podcasts and and stream it
## Getting Started
```sh
    docker-compose up -d
```
This will start elasticsearch and kibana. You can access kibana at http://localhost:5601

## Project Structure
```
- src/   - contains the source code for the project
   - feedservice/   - contains the feed parser   (in golang)  
   - searchservice/ - contains the search engine (in c#)

```