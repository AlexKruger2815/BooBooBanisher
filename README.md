![build succeeded](https://img.shields.io/badge/build-succeeded-brightgreen.svg)
![Test passing](https://img.shields.io/badge/Tests-passing-brightgreen.svg)
![Booboo Banished](https://img.shields.io/badge/Booboo%3F-Banished-brightgreen)
![Yee Haw](https://img.shields.io/badge/Yee-Haw-brightgreen)

# BooBooBanisher

Welcome to BooBooBanisher&copy;! 🎉

BooBooBanisher aims to sprinkle some motivation into your coding journey by displaying uplifting messages when you're trying to compile your code, whether it compiles successfully or it fails. Then you can look as peaceful as this person 🧘.

## How It Works

You can use our program to run terminal commands, more specifically to compile your code, and then it will give you a nice message to let you know whether your command was successful or not. Afterwards you have the option of seeing the actual result of your compilation. You can also get some fun statistics, like how many times your commands executed successfully! You also have the option of running our program through a CLI, or a GUI.

## Visual Demonstration

| Bear with booboo  | Bear after using BooBooBanisher |
| ------------- | ------------- |
| <img width="250" height="100" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJhWpF_xYQtaT3j-FoNRczguvMv7AMAL8rEMoo6khruw&s">  | <img width="250" height="100" src="https://akm-img-a-in.tosshub.com/indiatoday/images/story/202002/teddy-1444642_960_720_0.jpeg?size=690:388">  |


## Features

- Instant Motivation: Get a boost of encouragement precisely when you need it.
- Customizable Messages: Tailor motivational messages to fit your style.
- Lightweight: Doesn't add any extra weight to your workflow.

## Infrastructure Setup
Prerequisites:
* Terraform v1.5.7
* Terragrunt v0.53.2
1. Navigate to the ./infrastructure directory
2. Set these environment variables on your system for use when creating the DB:
    * `TF_VAR_username`: The username for your database.
    * `TF_VAR_password`: The password for your database.
4. Run this command to build the infrastructure:
   ``` sh
   terragrunt run-all apply
   ```

## Getting Started

To get started with BooBooBanisher, simply clone this repository and follow the installation instructions in the `README.md` (that is me, whom you are currently reading).

Prerequisites:
* Dotnet v8.0.104

```bash
git clone https://github.com/AlexKruger2815/BooBooBanisher.git
cd BooBooBanisher
```

### Running the API
```bash
cd bbb
dotnet run
```


## The Banishers
- Alex Kruger
- Dané De Klerk
- Joseph Kimathi
- Liam Quick
- Shane Theron

## Links
For more detailed documentation and tips, check out our [Confluence page](https://bbd-dane.atlassian.net/jira/software/projects/CLUB/boards/5/backlog).

Stay up-to-date with our project progress and tasks by visiting our [Jira board](https://bbd-dane.atlassian.net/jira/software/projects/CLUB/boards/5).

> :memo: **Note:** Sunrises are beautiful.
