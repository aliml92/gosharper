package main

import (
	"flag"
	"log"
	"time"

	"github.com/aliml92/gosharper/src/indexerservice/config"
	"github.com/go-co-op/gocron"
)

func main() {
	// get config
	conf, err := config.LoadConfig("dev", "./env")
	if err != nil {
		log.Fatalf("config load error: %v", err)
	}

	// configure number of concurrent scraper workers
	var n uint 
	flag.UintVar(&n, "worker-count", 4, "number of concurrent scraper workers; default 4")
	flag.Parse()

	if n > 10 {
		log.Fatalf("number of workers must be less than 10, got %d", n)
	}
	
	conf.WorkerCount = n
	jobs := make([]*gocron.Job, n)
	
	// create scheduler
	s := gocron.NewScheduler(time.UTC)
	for i:= 0; i < int(n); i++ {
		job, err  := s.Every(1).Day().At("5:50").Do(Indexer, i, conf)
		if err != nil {
			log.Fatalf("job create error: %v", err)
		}
		jobs[i] = job
	}

	log.Printf("jobs created: %d", len(jobs))
}


func Indexer(i int, conf config.Config) {
	log.Printf("worker %d started\n", i)
	
}