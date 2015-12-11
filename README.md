#Welcome to CompositeUI project

This project shows how to create composite UI in asp.net mvc in service oriented architecture style  

Each service has its own web application that can be developed independently from other service web application as if it was hosted on different server, but it is integrated into one process with all other servicie web applications.  

Code includes ready to use composite ui infrastructure component - download sources and include Service.Infrastructure project into your solution

Documentation: https://github.com/padzikm/CompositeUI/wiki

Features:
+ developing services independently from each other
+ integrating services' web applications into one web application in a transparent way
+ all services's web applications are in the same one process, but they behave and develop as if they were hosted on separate servers
+ changing one service is transparent to other services
+ scalling any subset of service actions - not only whole web applications and machines
+ caching can be used naturally at many levels
+ optimizing amount of server requests
+ adding new services in transparent way
+ independent from asp.net mvc version
