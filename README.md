# Learning Kafka

A learning project for Kafka.

# Getting Started

**Requirements**

* Docker installed.

**Steps**

Include service entries in local [hosts](file://C:\Windows\System32\drivers\etc\hosts) file:

```
127.0.0.1               zookeeper               # ZooKeeper (single node)
127.0.0.1               kafka                   # Kafka (single node)

127.0.0.1               zookeeper-1             # ZooKeeper (cluster)
127.0.0.1               zookeeper-2             # ZooKeeper (cluster)
127.0.0.1               zookeeper-3             # ZooKeeper (cluster)
127.0.0.1               kafka-1                 # Kafka (cluster)
127.0.0.1               kafka-2                 # Kafka (cluster)
127.0.0.1               kafka-3                 # Kafka (cluster)
```

Clone the repository.

To start a single-node Kafka instance:

* Open a command prompt.
* Change to the `/broker` directory.
* Run `docker-compose --file kafka-single-node.yml up` command.

To start a multi-node Kafka cluster:

* Open a command prompt.
* Change to the `/broker` directory.
* Run `docker-compose --file kafka-cluster.yml up` command.

To access Kafka Manager:

* Open a browser and navigate to http://localhost:9000/.
* If not already configured, create an entry for the local development cluster.
  * Specify `zookeeper:2181` as the host and select the latest Kafka version.

To run the test client:

* TBD
