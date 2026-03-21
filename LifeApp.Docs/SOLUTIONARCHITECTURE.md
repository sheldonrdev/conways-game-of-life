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

### 2.1. Functional Requirements (FRs)

#### 2.1.1. Application
| ID | Requirement                                                                                                                                           |
|----|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| FR-01 | The application SHALL implement Conway's Game of Life according to the standard ruleset: underpopulation, survival, overpopulation, and reproduction. |
| FR-02 | The application SHALL generate a random placement of cells at the start.                                                                              |
| FR-03 | The application SHALL support a configurable board size.                                                                                              |
| FR-04 | The application SHALL support a configurable number of generations.                                                                                   |
| FR-05 | The application SHALL display each generation to the standard console output.                                                                         |

#### 2.1.2. Infrastructure
| ID    | Requirement                                                                                              |
|-------|----------------------------------------------------------------------------------------------------------|
| FR-06 | A custom VPC SHALL be provisioned in the eu-west-1 (Ireland) region.                                     |
| FR-07 | The VPC SHALL contain two public subnets, each in a separate availability zone.                          |
| FR-08 | Both subnets SHALL have internet access                                                                  |
| FR-09 | Two Linux-based EC2 instances SHALL be provisioned, one in each public subnet.                           |
| FR-10 | Each EC2 instance SHALL run a web server that returns the hostname and IP address of the current server. |
| FR-11 | An Application Load Balancer SHALL distribute HTTP traffic across both EC2 instances.                    |
| FR-12 | A read-only IAM user with console-only access SHALL be created for reviewer access.                      |

#### 2.1.3. Out-of-scope 
| ID     | Requirement                                                   |
|--------|---------------------------------------------------------------|
| OFR-01 | Application deployment to the AWS Infrastructure.             |
| OFR-02 | Infrastructure provisioning via Infrastructure as Code (IAC). |

### 2.2. Non-Functional Requirements (NFRs)

#### 2.3.1. General
| ID     | Requirement                                                                        |
|--------|------------------------------------------------------------------------------------|
| NFR-01 | The repository SHALL maintain an incremental git history demonstrating the evolution of the solution. |
| NFR-02 | A README SHALL enable anyone to build, test, and run the application.              |
| NFR-03 | A SOLUTIONARCHITECTURE.md SHALL the various SDLC activities.                       |

#### 2.2.2. Application
| ID     | Requirement                                                                 |
|--------|-----------------------------------------------------------------------------|
| NFR-04 | The application SHALL build and run via dotnet CLI using .NET 10 (LTS) SDK. |
| NFR-05 | The core game engine SHALL enable isolated unit testing.                    |

#### 2.2.3. Infrastructure
| ID     | Requirement                                                                                                     |
|--------|-----------------------------------------------------------------------------------------------------------------|
| NFR-06 | All AWS resources SHALL remain within the AWS Free Tier.                                                        |
| NFR-07 | The provisioning IAM user SHALL follow the principle of least privilege. The root account SHALL not be used.    |
| NFR-08 | Security groups SHALL restrict EC2 inbound traffic to the ALB only; the ALB SHALL accept inbound traffic (HTTP) |

#### 2.2.4. Out-of-Scope
| ID      | Item                                                         |
|---------|--------------------------------------------------------------|
| ONFR-01 | Scaling (Auto-scaling, vertical or horizontal scaling).      |
| ONFR-02 | Recovery, failover, or self-healing infrastructure.          |
| ONFR-03 | Infrastructure performance targets.                          |
| ONFR-03 | Infrastructure performance targets.                          |
| ONFR-04 | Observability (traces, logs, metrics, monitoring, alerting). |
| ONFR-05 | HTTPS/TLS termination or custom domain configuration. (TBC). |

## 3. Research & Analysis
### 3.1. Conway's Game of Life
The provided [Wikipedia page](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) describes it as a zero-player cellular automaton devised by mathematician John Conway in 1970. 
The game evolves from an initial state with no further input, governed by four deterministic rules applied simultaneously to every cell each generation.

#### 3.1.1 Core Components
| Component    | Description                                                                |
|--------------|----------------------------------------------------------------------------|
| Grid         | A two-dimensional grid of cells. Configurable dimensions (rows × columns). |
| Cell         | The basic unit of the grid. Binary states: **alive** or **dead**.          |
| Neighbours   | The immediately adjacent cells surrounding a given cell.                   |

NB.
1. Grid does not have to be square.
2. Cell + Neighbours form a 3x3 grid therefore max 8 neighbours per cell.
3. Cells at edge are evaluated therefore neighbours for cells at edge < 8.

#### 3.1.2. Rules
This [Ruleset reference](https://playgameoflife.com/info) provides a visual representation of the rules which can be summarised as follows:

| Rule             | Condition                                    | Outcome    |
|------------------|----------------------------------------------|------------|
| Underpopulation  | Live cell with fewer than 2 live neighbours. | Cell dies  |
| Survival         | Live cell with 2 or 3 live neighbours.       | Cell lives |
| Overpopulation   | Live cell with more than 3 live neighbours.  | Cell dies  |
| Reproduction     | Dead cell with exactly 3 live neighbours.    | Cell is born |

NB. 
1. All rules are applied simultaneously.
2. The next generation is computed from the current generation.
3. Only a cells immediate (directly in contact) neighbours are considered.

#### 3.1.3. Demo
This [Interactive demo](https://playgameoflife.com/) provides a demonstration of the game and its rules in action.
You are able to slow down the generation to a point where you can observe the rules being applied.

## 4. Architecture

## 4.1 Decisions
| Decision | Context                                                                                           |
|--------------|-----------------------------------------------------------------------------------------------|
| HTTP only | HTTPS would require ACM + Route53 or a self-signed cert with no practical benefit for this task. |

## 4.2. Design

### 4.2.1. Application
![App Arch](assets/application-design.drawio.svg)

### 4.2.2. Game Engine
![Algorithm Flow](assets/game-engine-design.drawio.svg)

### 4.2.3. Infrastructure
TODO

## 5. Development

## 6. Infrastructure

## 7. Testing

## 8. References


