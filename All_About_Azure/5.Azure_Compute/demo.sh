REducing the cost:
--------------------------->

1. Auto Shutdown
2. Reserved Instances
3. Spot Instances
4. Disk Optimization

Disk:
premium SSD
standard SSD  --> App Servers and In-memory cache


Availability of VMs:
--------------------------->
Fault Domain  --> one rack --> its a physical hardware ... i.e. server rack
Update Domain  --> Host machine reboot  --> its a logical definiton

So naturallu we want to make sure our servers are spread across more than one Fault Domain(more than one rack)  and more than one Update domain

Availability Set:
Is a collection of Fault Domains and update domains 

Availability Zone
