"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import { Input } from "../ui/input";
import { Button } from "../ui/button";
import { Search } from "lucide-react";

const formSchema = z.object({
  ModelName: z.string().optional(),
  BrandName: z.string().optional(),
});

type FormSchemaType = z.infer<typeof formSchema>;

const SearchCarsForm = () => {
  const form = useForm<FormSchemaType>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      BrandName: "",
      ModelName: "",
    },
  });

  const onSubmit = (values: FormSchemaType) => {
    console.log("perform search operation: ", values);
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="relative mx-auto flex flex-col items-center justify-center space-y-8 lg:flex-row lg:items-end lg:space-x-5 lg:space-y-0"
      >
        <FormField
          control={form.control}
          name="BrandName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Brand name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="ModelName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Model name</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <Button type="submit">
          <Search className="mr-2 h-4 w-4" />
          Search
        </Button>
      </form>
    </Form>
  );
};

export default SearchCarsForm;
