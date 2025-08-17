import { CanDeactivateFn } from '@angular/router';
import { MemberProfile } from '../../features/members/member-profile/member-profile';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberProfile> = 
(component) => {
  if (component.editForm?.dirty) {
    return confirm('R u sure u want to contine? All Unsaved changes will be lost');
  }
  return true;
};
