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
Whilst researching Conway's Game of Life and processing its rules and considering my approach it became clear that I could draw on knowledge from both my undergrad studies and professional career. 
Coming from an Electronic Engineering background, I naturally interpretted this problem through the lens of digital signal (image) processing and matrix mathematics. 

Whilst I wrote my university thesis over a decade ago, long before AI existed in its current form, the fundamental knowledge I developed then provided the intuition for how to architect this system today.

This is documented in detail in [Application Decisions](#411-application) below and references my [Engineering Thesis](https://1drv.ms/b/c/f5e6b5f19a1ec68c/IQCfZ1nfTJGbSZo-6bI-l3F4AQSQXjkSfHigr1XTs2SqOJk?e=ZUqqqX).

## 4.1 Decisions

### 4.1.1. Application

#### 4.1.1.1. Grid
As I noted in *Section 4.1* of my [Engineering Thesis](https://1drv.ms/b/c/f5e6b5f19a1ec68c/IQCfZ1nfTJGbSZo-6bI-l3F4AQSQXjkSfHigr1XTs2SqOJk?e=ZUqqqX), MATLAB stands for *Matrix Laboratory*, and I chose it as my IDE because of its basic data element, the matrix. 

I later extended my MATLAB experience during tenure at Opti-Num Solutions. 
Whilst developing the MATHLAB OPC UA Toolboxfor Mathworks, I had to bridge traditional object-oriented C++ architecture with MATLAB's foundational vector types.

That experience directly influenced my C# architecture for this assessment. 
Rather than leverage objects (OOP), which live on the heap, for each `Cell`, I chose the humble 2D array. 
Objects would also work well but ultimately would burden the garbage collector at scale, introducing performance issues.

In C#, a 2D array is the closest high-performance analogous to a Matlab matrix. 
This data structure ensures excellent CPU cache locality, constant time `O(1)` access and predictable memory allocation (continguous memory locations), keeping the core game engine fast and lightweight.

Grid creation is done by means of a very simple Factory method (strictly speaking *NOT* GOF Factory Pattern) given it's purpose is purely creational.

##### 4.1.1.1.1. Grid Randomness
In my current role within the iGaming industry, *Random Number Generators* (RNGs) are critical elements which must be certifiable, auditable, and reproducible for regulatory compliance. 
From my experience at GamesGlobal, I understand the importance of being able to reproduce a sequence of "random" outcomes given the same initial conditions.

I applied this principle to the grid generation by supporting an optional seed parameter. 
1. When provided, the same seed produces an identical initial grid every time, enabling deterministic test cases which would otherwise fail and drive one nuts. 
2. When omitted, the system defaults to non-deterministic randomness for normal execution.

#### 4.1.1.2. Neighbours
My [Engineering Thesis](https://1drv.ms/b/c/f5e6b5f19a1ec68c/IQCfZ1nfTJGbSZo-6bI-l3F4AQSQXjkSfHigr1XTs2SqOJk?e=ZUqqqX) focused on High-Dynamic-Range (HDR) imaging. 
At the time commercial HDR monitors did not exist and so I had to compress HDR image maps into standard dynamic range (SDR) images for display on our conventional monitors.
This allowed me to computationally process HDR images and once the main objective (Feature detection in extreme lighting conditions) was achieved, I could map the result into a form (pseudo HDR) that could be displayed on our monitors.
To achieve this, I wrote an algorithm which applied a 2D NxN convolutional matrix (Gaussian filter), with a Chebyshev distance of *n* from its centre, over an image to be able resolve the contrast local to the matrix.

Now, Conway's game requires you to count the alive neighbours adjacent to a cell. 
This results in a 3x3 matrix (1 Centre Cell + 8 Neighbours) being resolved for every cell (except cells at the edge) on the grid.
This counting is not just a nested loop with an if statement; conceptually, it is a 2D 3×3 (N=3) convolutional matrix (with Chebyshev distance = 1 from its centre) sliding over a 2D grid. 
This *unique insight and experience* led me to the [Moore Neighbourhood](https://en.wikipedia.org/wiki/Chebyshev_distance) which is essentially a 2D 3x3 matrix with a Chebyshev distance of 1 from the center.

By treating the grid like an image where each block is analogous to an image pixel and utilising the Moore Neighbourhood which is analogous to a 2D 3x3 convolutional matrix, I am able to achieve two things in the application
1. Ensure all rules are applied simulataneous for every cell being resolved, as mentioned in [Section 3.1.2](#312-rules)
2. Decouple game engine execution from grid traversal (testing and scaling benefits) which allows for separation of concerns and single responsibility (SOLID principles) in the code.

#### 4.1.1.3 Generation Iterations
I chose to return a new grid from `GetNextGeneration` rather than modifying the input grid in-place. 
This ensures every cell is evaluated against the same generation iteration and prevents processing a grid with a mix of current and next generation state which would mess up the `GetLiveNeighbours` result.

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
The development of the [Application](#421-application) follows an interative approach, building up and around the fundamental logic unit, the [Game Rules](#312-rules).

### 5.1. Iterations
| Component    | Detail                                                                   |
|--------------|--------------------------------------------------------------------------|
| Game Rules   | Static class to apply the rules based on the of neigbours.               |
| Grid Factory | Static class which creates a random grid with optional seeding.          |
| Game Engine  | Instantiable service encapsulating relevant game logic and functionality |


## 6. Infrastructure

## 7. Testing

## 8. References


