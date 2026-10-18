# GitHub Copilot Instructions for the `Apparel Tab+ (Continued)` Mod

## Mod Overview and Purpose
The `Apparel Tab+ (Continued)` mod enhances the player's experience by enabling better navigation and evaluation of apparel items across all mods in your RimWorld modpack. This is a continuation of DimonSever000's original mod. It adds a feature to the bottom menu of the game where players can assess apparel characteristics even if they are not on the player's map.

## Key Features and Systems
- **Comprehensive Apparel Evaluation**: An additional feature in the game’s interface allows users to inspect apparel stats conveniently.
- **Cross-Mod Apparel Assessment**: Check and filter apparel items from any mod integrated into your game.
- **Vanilla Info Tab Access**: Simply left-clicking on any apparel item opens the vanilla information tab for that item, maintaining a consistent user interface experience.
- **Sequential Filtering System**: Apparel filtering is processed from left to right for intuitive navigation.
- **Customizable UI Elements**: Features like buttons and filter elements are part of the GUI customization.

## Coding Patterns and Conventions
- **Clean C# Structure**: The mod consists of structured C# classes, each with specific responsibilities such as game component management, GUI filter implementation, and utility functions.
- **Consistent Naming Conventions**: Classes and methods are named with clear, descriptive identifiers (e.g., `GameComponent_ApparelTab`, `FilteredApparels`).
- **Encapsulation and Separation of Concerns**: Each class handles distinctly separate functionality to facilitate easier maintenance and scalability.

## XML Integration
The mod employs XML to define UI components and register them within the game:
- **MainButtonDef.xml**: Custom main button definitions are added here, which integrates with the game's main button menu interface.
- Ensure that the XML files are validated for correct syntax and adhere to RimWorld's XML structure conventions.

## Harmony Patching
- **Non-Invasive Modifications**: Harmony is used to patch existing game methods safely. Ensure patches are reversible and only affect necessary game aspects.
- **Patch Definitions**: Include `[HarmonyPatch]` attributes above the targeted methods to inject enhancements without altering the base game code.
- Consider adding safety checks and logging within patches to aid in debugging and maintaining game stability.

## Suggestions for Copilot
- **Code Suggestions**: Copilot can help by auto-suggesting patterns for new GUI elements or Harmony patches based on existing files.
- **Refactoring Assistance**: Utilize Copilot's capabilities to refactor repeated code blocks, ensuring code DRYness (Don't Repeat Yourself).
- **XML Structuring**: Copilot can aid in the creation of XML node structures by predicting required nodes and attributes.
- **Error Handling**: Suggest improved error-handling mechanisms for stability, especially when interacting with other mods’ data.
- **Documentation**: Generate comments and documentation for new methods and classes to improve code readability and maintainability.

By complying with these instructions and leveraging Copilot’s features, developers can efficiently maintain and expand the capabilities of the `Apparel Tab+ (Continued)` mod while ensuring robust performance and seamless integration with RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

