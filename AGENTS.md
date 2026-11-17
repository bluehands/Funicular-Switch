# Agent notes

## Plans
- `plans/open/` holds planned or deferred work. Read a plan only when your task relates to it.
- `plans/done/` holds implemented plans, kept as history. Do not read them by default: the code, tests and `docs/` are authoritative. Read one only when you change code in its scope (see the `Scope:` line at the top of the plan), for example to understand why a decision was made.
- When a plan is implemented, move it to `plans/done/`, add the header (status, scope, decisions worth knowing) and update `plans/README.md`.
