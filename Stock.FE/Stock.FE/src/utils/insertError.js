import { validate } from "./validate";
import MResources from "@/helper/resources";

/**
 * Function to insert error for a form input
 * @param {Object} inputRef - The input reference
 * @param {string} entity - The entity name
 * @returns {string} - The error message for the input
 * @author nttue  07.07.2023
 */
export default function insertError(inputRef, entity) {
  try {
    let errorMessage = "";
    const rulesList = inputRef?.rules.split("|");
    for (let rule of rulesList) {
      errorMessage = inputRef?.typeError
        ? validate[rule](
            inputRef.inputValue,
            MResources[entity][inputRef.name][rule]
          )
        : validate[rule](
            inputRef.modelValue,
            MResources[entity][inputRef.name][rule]
          );
      if (errorMessage) return errorMessage;
    }
    return errorMessage;
  } catch (error) {
    console.error(error);
  }
}
