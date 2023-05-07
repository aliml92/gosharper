CREATE TABLE IF NOT EXISTS "podcasters" (
  "id"               text not null,
  "feed_url"         text not null,
  "website_url"      text not null,
  "description"      text,
  "thumbnail_image"  text,

  "is_active"        boolean not null default true,
  "is_auth_required" boolean,  
  "is_paid"          boolean,
  
  "added_at"         timestamp not null default now(),
  "updated_at"       timestamp not null default now(),

  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS "feed_partitions" (
  "id"                   text not null,
  "feed_partition_url"   text not null,
  "podcaster_id"         text not null,
  "etag"                 text,
  "last_modified"        timestamp,
  
  "added_at"             timestamp not null default now(),
  "updated_at"           timestamp not null default now(),

  PRIMARY KEY (id),
  FOREIGN KEY (podcaster_id) REFERENCES podcasters(id)  
);

CREATE TABLE IF NOT EXISTS "feeds" (
  "id"                   text not null,
  "feed_partition_id"    text not null,
  "podcaster_id"         text not null,  
  "published"            timestamp not null, 
  "updated"              timestamp,          

  "added_at"             timestamp not null default now(),
  "updated_at"           timestamp not null default now(),

  PRIMARY KEY (id),
  FOREIGN KEY (podcaster_id) REFERENCES podcasters(id)  
);