# Car Rent Frontend

## Prerequisites

- `node` version >= `20.18.0`
- `npm` version >= `10.9.0`

## Installation

In your terminal run:

```shell
npm install
```

## Development mode

In order to run application in development mode run following command:

```shell
npm run dev
```

## Release mode

To run application in release mode run following command:

```shell
npm run build && npm run start
```

## Linting

If you are using editor that supports prettier (e.g. VS Code) by default on save your files should be automatically formatted. If it's not the case, please run `npm run lint` before creating pull request.

## Components

We use [shadcn/ui](https://ui.shadcn.com/docs) as our components library. It works in a way that you only install the components you need. When you want to use some component, e.g. [Button](https://ui.shadcn.com/docs/components/button) and it's not already installed in your project then you must do it yourself, in this case it would be:

```shell
npx shadcn@latest add button
```

You can check currently installed components at `src/components/ui`.

For list of all available components and how to use them please refer to [shadcn/ui Components](https://ui.shadcn.com/docs/components/).

## Data fetching from / sending to API

We use for [TanStack Query](https://tanstack.com/query/latest/docs/framework/react/overview) for managing fetching and sending data. If you are about to work on some functionality that is related to it, please follow the docs.
For devtools with debug information about queries/mutations, you can click TanStack Query icon in the right bottom corner (they are only available in development mode).
