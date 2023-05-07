package config

import "github.com/spf13/viper"


type Config struct {
	Environment     string	`mapstructure:"ENVIRONMENT"`
	Host            string  `mapstructure:"HOST"`
	Port            string	`mapstructure:"PORT"`

	DBUsername      string	`mapstructure:"DB_USERNAME"`
	DBPassword      string	`mapstructure:"DB_PASSWORD"`
	DBHost          string	`mapstructure:"DB_HOST"`
	DBPort          string	`mapstructure:"DB_PORT"`
	DBName          string	`mapstructure:"DB_DBNAME"`
	DBUrl           string

	DBMigrationPath string	`mapstructure:"DB_MIGRATION_PATH"`
	DBRecreate      bool  	`mapstructure:"DB_RECREATE"`

	ESHost          string	`mapstructure:"ES_HOST"`
	ESPort          string	`mapstructure:"ES_PORT"`
	ESIndex         string	`mapstructure:"ES_INDEX"`
	ESUrl           string

	WorkerCount     uint	`mapstructure:"WORKER_COUNT"` 
}

func LoadConfig(name string, path string) (Config, error) {
	viper.AddConfigPath(path)
	viper.SetConfigName(name)
	viper.SetConfigType("env")

	viper.AutomaticEnv()

	if err := viper.ReadInConfig(); err != nil {
		return Config{}, err 
	}
	var config Config
	if err := viper.Unmarshal(&config); err != nil {
		return Config{}, err
	}
	return config, nil
}