# STMS
Strait Traffic Monitoring Simulation (STMS) is a sample federation application for High Level Architecture, which serves as a running-example for the book "Guide to Distributed Simulation Using HLA" (see https://link.springer.com/book/10.1007/978-3-319-61267-6).

## Scenario
A traffic monitoring station tracks the ships passing through the strait. Any ship entering the strait announces her name and then periodically reports her position to the station and to the other ships in the strait using the radio channels. Channel-1 is used for ship-to-ship and channel-2 is used for ship-to-shore communication. The traffic monitoring station tracks ships and ships track each other through these communication channels. All radio messages are time-stamped to preserve the transmission order.
![img](https://lh6.googleusercontent.com/8XixSp7yqkbOjH6GcWtQ1MiZEi2LoBfDQgAN4r-zOD7LQgPCN5xDO7UVm2l4o7PwSkNucaAoQgfqyuENiOxBXFT1XRPFr9nMPLjwTtA848jr2XWq=w1280)

## More Links
* RACoN: https://sites.google.com/view/okantopcu/racon
* STMS web site:  https://sites.google.com/view/distributed-simulation/stms

## Release History
0.0.2.5:
* Compatible with RACoN 0.0.2.5/0.0.2.6.

0.0.2.4:
* Compatible with RACoN 0.0.2.4.