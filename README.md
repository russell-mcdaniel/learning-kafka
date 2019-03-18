# Learning Kafka

A learning project for Kafka.

# Getting Started

**Requirements**

* Docker installed.

**Steps**

Include service container entries in local [hosts](file://C:\Windows\System32\drivers\etc\hosts) file:

```
127.0.0.1               kafka                   # Local Kafka instance
127.0.0.1               zookeeper               # Local Zookeeper instance
```

Clone the repository.

To start a local Kafka broker:

* Open a command prompt.
* Change to the `/broker` directory.
* Run `docker-compose up` command.

To access Kafka Manager:

* Open a browser and navigate to http://localhost:9000/.
* If not already configured, create an entry for the local development cluster.
  * Specify `zookeeper:2181` as the host and select the latest Kafka version.

To run the test client:

* TBD
