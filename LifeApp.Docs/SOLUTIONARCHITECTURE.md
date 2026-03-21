# CGOL: Solution Architecture

## 1. Overview

### 1.1. Context
This solution comprises of two independent deliverables:
1. **Application**: Conway's Game of Life with configurable board size and generations.
2. **Infrastructure**: AWS hosted serving an endpoint that returns hostname and IP address.

### 1.2. Constraints
The solution is constrained to the AWS Free Tier, single OS-based compute, and a 7-day delivery window with limited developer capacity.

## 2. Requirements
The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](https://datatracker.ietf.org/doc/html/rfc2119).

### 2.1. Functional Requirements (FR)

#### 2.1.1. Application
| ID | Requirement                                                                                                                                          |
|----|------------------------------------------------------------------------------------------------------------------------------------------------------|
| FR-01 | The application SHALL implement Conway's Game of Life acording to the standard ruleset: underpopulation, survival, overpopulation, and reproduction. |
| FR-02 | The application SHALL generate a random placement of cells at the start.                                                                             |
| FR-03 | The application SHALL support a configurable board size.                                                                                             |
| FR-04 | The application SHALL support a configurable number of generations.                                                                                  |
| FR-05 | The application SHALL display each generation to the standard console output.                                                                        |

#### 2.1.2. Infrastructure
| ID    | Requirement                                                                                              |
|-------|----------------------------------------------------------------------------------------------------------|
| ID    | Requirement                                                                                              |
| ----  | -------------                                                                                            |
| FR-07 | A custom VPC SHALL be provisoined in the eu-west-1 (Ireland) region.                                     |
| FR-08 | The VPC SHALL contain two public subnets, each in a separate availablity zone.                          |
| FR-09 | Both subnets SHALL have internet access                                                                  |
| FR-10 | Two Linux-based EC2 instances SHALL be provisioned, one in each public subnet.                           |
| FR-11 | Each EC2 instance SHALL run a web server that returns the hostname and IP address of the current server. |
| FR-12 | An Application Load Balancer SHALL distrbute HTTP traffic across both EC2 instances.                    |
| FR-13 | A read-only IAM user with console-only access SHALL be created for reviewer access.                      |

#### 2.1.3. Out-of-scope 
| ID     | Requirement                                                   |
|--------|---------------------------------------------------------------|
| OFR-01 | The application MAY be deployed to the AWS Infrastructure.    |
| OFR-02 | Infrastructure MAY be provisioned via Infrastructure as Code. |


### 2.2. Non-Functional (NFR)
| ID | Requirement |
|----|-------------|


#### 2.2.1. Application
| ID | Requirement |
|----|-------------|


#### 2.3.2. Infrastructure

## 3. Research & Analysis

## D4.esign

## 5. Development

## 6. Infrastructure

## 7. Testing

## 8. References


