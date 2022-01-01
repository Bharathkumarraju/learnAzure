# Secure azure virtual networks
Secure Virtual Networks 

NSGs --> Network Security Groups 
ASGs --> Application Security Groups 

Defence-in-depth

NSGs allow or deny inbound and outbound traffic.
NSGs contain rules, rules are ordered based on a number from 100(processed first) to 4096(processed last).

NSGs are stateful.


Solving NSG Problems:
------------------------------>
Use service Tags  --> Represents services like Azure load balancer or API management and locations like internet
Use the default security rules 
Use Application Security Groups

