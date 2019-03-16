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
* Change to the /src/broker directory.
* Run `docker-compose up` command.

To run the test harness:

* TBD

# Design

**Producer**

Each producer instance posts messages to one topic.

Command Line

> `producer.exe --topic "topic-name" --partitions # --rate # --size #`

* topic. Target topic for messages.
* partitions. Posts messages to partitions in a round-robin fashion.
* rate. Messages per minute.
* size. Message size in bytes. Consider support for randomization range.

**Consumer**

> `consumer.exe --topic "topic-name" --consumerid "consumer-id" --groupid "{group-id}"`

**Coordinator**

Launches a set of producer and consumer instances as specified in the run scenario file.
